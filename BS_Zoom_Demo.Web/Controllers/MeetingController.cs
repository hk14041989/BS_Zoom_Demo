using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BS_Zoom_Demo.Web.Controllers
{
    public class MeetingController : BS_Zoom_DemoControllerBase
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly string userId = "ifusezvfS5mT-u2MhPoa1g";

        // GET: Meeting
        public ActionResult Index()
        {
            ViewBag.newJWTToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJhdWQiOm51bGwsImlzcyI6InZTX0U0c0szUzd1RkVKazZJNzZLcnciLCJleHAiOjE2MDUyMzY4NzksImlhdCI6MTYwNDYzMjA4Mn0.EewFFC3M3id1AIs9TuDPo2JLMSABrUzoGniXlcgbt6s";
            return View();
        }

        [HttpPost]
        public JsonResult CreateMeeting(string accessToken, string topicName, int meetingType, string startTime, int duration, string scheduleFor,
            string timeZone, string password, string agenda, int recurrenceType, int repeatInterval, string weeklyDays, int monthlyDay, int monthlyWeek,
            int monthlyWeekDay, int endTimes, string endDateTime, bool hostVideo, bool participantVideo, bool cnMeeting, bool inMeeting, bool joinBeforeHost,
            bool muteUponEntry, bool watermark, bool usePmi, bool enforceLogin, string enforceLoginDomains, int approvalType, int registrationType, string audio, string autoRecording, string alternativeHosts, bool closeRegistration,
            bool waitingRoom, string globalDialInOuntries, string contactName, string contactEmail, bool registransEmailNotification, bool meetingAuthentication)
        {
            try
            {
                var client = new RestClient("https://api.zoom.us/v2/users/" + userId + "/meetings");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", "Bearer " + accessToken);

                string PostData = new JavaScriptSerializer().Serialize(new
                {
                    topic = topicName,
                    type = meetingType,
                    start_time = startTime,
                    duration,
                    schedule_for = scheduleFor,
                    timezone = timeZone,
                    password,
                    agenda,
                    recurrence = new
                    {
                        type = recurrenceType,
                        repeat_interval = repeatInterval,
                        weekly_days = weeklyDays,
                        monthly_day = monthlyDay,
                        monthly_week = monthlyWeek,
                        monthly_week_day = monthlyWeekDay,
                        end_times = endTimes,
                        end_date_time = endDateTime,

                    },
                    setting = new
                    {
                        host_video = hostVideo,
                        participant_video = participantVideo,
                        cn_meeting = cnMeeting,
                        in_meeting = inMeeting,
                        join_before_host = joinBeforeHost,
                        mute_upon_entry = muteUponEntry,
                        watermark,
                        usePmi,
                        approval_type = approvalType,
                        registration_type = registrationType,
                        audio,
                        auto_recording = autoRecording,
                        enforce_login = enforceLogin,
                        enforce_login_domains = enforceLoginDomains,
                        alternative_hosts = alternativeHosts,
                        global_dial_in_countries = new string[]
                        {
                            globalDialInOuntries,
                        },
                        registrants_email_notification = registransEmailNotification
                    },
                });

                request.AddParameter("application/json", PostData, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    dynamic data = JObject.Parse(response.Content);
                    return OK(new { meeting_id = data.id, data.password, data.start_url }, "Create Done!");
                }
                else
                    return OK(new { meeting_id = "", password = "", start_url = "" }, response.ErrorMessage);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex.ToString());
            }
        }

        public ActionResult JoinMeetingHost()
        {
            return View("_Meeting");
        }

        public ActionResult GenerateNewJWTToken()
        {
            var zoomToken = new ZoomToken("vS_E4sK3S7uFEJk6I76Krw", "1d5M36uFNCg2zwdhkiJ2bOP1D1Ej67dU8ZKP");
            string newJWTToken = zoomToken.Token;
            return OK(new { newJWTToken });
        }
    }

    public class ZoomToken
    {
        public ZoomToken(string ZoomApiKey, string ZoomApiSecret)
        {
            DateTime Expiry = DateTime.UtcNow.AddMinutes(5);
            string ApiKey = ZoomApiKey;
            string ApiSecret = ZoomApiSecret;

            int ts = (int)(Expiry - new DateTime(1970, 1, 1)).TotalSeconds;

            // Create Security key  using private key above:
            // note that latest version of JWT using Microsoft namespace instead of System
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ApiSecret));

            // Also note that securityKey length should be >256b
            // so you have to make sure that your private key has a proper length
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Finally create a Token
            var header = new JwtHeader(credentials);

            //Zoom Required Payload
            var payload = new JwtPayload
            {
                { "iss", ApiKey},
                { "exp", ts },
            };

            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            // Token to String so you can use it in your client
            var tokenString = handler.WriteToken(secToken);
            //string Token = tokenString;
            this.Token = tokenString;
        }

        public string Token { get; set; }
    }
}