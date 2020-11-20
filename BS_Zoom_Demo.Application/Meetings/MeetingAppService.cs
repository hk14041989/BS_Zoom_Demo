﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using AutoMapper;
using BS_Zoom_Demo.Common;
using BS_Zoom_Demo.Meetings.Dtos;
using BS_Zoom_Demo.Teachers;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace BS_Zoom_Demo.Meetings
{
    public class MeetingAppService : ApplicationService, IMeetingAppService
    {
        //These members set in constructor using constructor injection.

        private readonly IMeetingRepository _meetingRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IMapper _mapper;        
        private static readonly HttpClient client = new HttpClient();

        /// <summary>
        ///In constructor, we can get needed classes/interfaces.
        ///They are sent here by dependency injection system automatically.
        /// </summary>
        public MeetingAppService(IMeetingRepository meetingRepository, IRepository<Person> personRepository, IMapper mapper)
        {
            _meetingRepository = meetingRepository;
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public GetMeetingsOutput GetMeetings(GetMeetingsInput input)
        {
            var result = new GetMeetingsOutput();

            //Called specific GetAllWithPeople method of meeting repository.
            var meetings = _meetingRepository.GetAllWithTeachers(input.AssignedPersonId, input.State);

            result.Meetings = new List<MeetingDto>();

            foreach(var item in meetings)
            {
                MeetingDto temp = new MeetingDto
                {
                    Id = item.Id,
                    MeetingId = item.MeetingId,
                    MeetingPass = item.MeetingPass,
                    StartTime = item.StartTime,
                    State = item.State,
                    TopicName = item.TopicName,
                    AccessToken = item.AccessToken,
                    AssignedPersonId = item.AssignedPerson != null ? item.AssignedPerson.Id : 0,
                    AssignedPersonName = item.AssignedPerson != null ? item.AssignedPerson.Name : "",
                    CreationTime = item.CreationTime,
                    Description = item.Description,
                    Duration = item.Duration,
                    EndTime = item.EndTime
                };

                result.Meetings.Add(temp);
            }
          
            return result;
        }

        public void UpdateMeeting(UpdateMeetingInput input, string meetingPass)
        {
            //We can use Logger, it's defined in ApplicationService base class.
            Logger.Info("Updating a meeting for input: " + input);

            //Retrieving a meeting entity with given id using standard Get method of repositories.
            var meeting = _meetingRepository.Get(input.MeetingId);

            var client = new RestClient("https://api.zoom.us/v2/meetings/" + meeting.MeetingId);
            var request = new RestRequest(Method.PATCH);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Bearer " + meeting.AccessToken);
            request.AddParameter("application/json", "", ParameterType.RequestBody);

            string PostData = new JavaScriptSerializer().Serialize(new
            {
                topic = meeting.TopicName,
                type = 2,
                meeting.StartTime,
                meeting.Duration,
                schedule_for = "",
                timezone = "Asia/Saigon",
                meeting.MeetingPass,
                meeting.Description,
                recurrence = new
                {
                    type = 1,
                    repeat_interval = 1,
                    weekly_days = 1,
                    monthly_day = 1,
                    monthly_week = 1,
                    monthly_week_day = 1,
                    end_times = 1,
                    meeting.EndTime,

                },
                setting = new
                {
                    host_video = false,
                    participant_video = false,
                    cn_meeting = false,
                    in_meeting = false,
                    join_before_host = true,
                    mute_upon_entry = false,
                    watermark = false,
                    usePmi = false,
                    approval_type = 2,
                    registration_type = 1,
                    audio = "both",
                    auto_recording = "none",
                    enforce_login = "",
                    enforce_login_domains = "",
                    alternative_hosts = "",
                    global_dial_in_countries = new string[]
                {
                        "",
                },
                    registrants_email_notification = false
                },
            });

            request.AddParameter("application/json", PostData, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var zoomToken = new ZoomToken(Const.apiKey, Const.apiSecret);
                string newJWTToken = zoomToken.Token;
                request = new RestRequest(Method.GET);
                request.AddHeader("authorization", "Bearer " + newJWTToken);
                request.AddParameter("application/json", "", ParameterType.RequestBody);

                request.AddParameter("application/json", PostData, ParameterType.RequestBody);
                response = client.Execute(request);
            }

            //Updating changed properties of the retrieved meeting entity.
            if (input.State.HasValue)
                meeting.State = input.State.Value;

            if (input.AssignedPersonId.HasValue)
                meeting.AssignedPerson = _personRepository.Load(input.AssignedPersonId.Value);

            if (!string.IsNullOrEmpty(input.topic_name))
                meeting.TopicName = input.topic_name;

            if (!string.IsNullOrEmpty(meetingPass))
                meeting.MeetingPass = meetingPass;

            if (!string.IsNullOrEmpty(input.start_time))
                meeting.StartTime = DateTime.Parse(input.start_time);

            if (!string.IsNullOrEmpty(input.end_date_time))
                meeting.EndTime = DateTime.Parse(input.end_date_time);

            if (!string.IsNullOrEmpty(input.agenda))
                meeting.Description = input.agenda;

            meeting.Duration = input.duration;

            //We even do not call Update method of the repository.
            //Because an application service method is a 'unit of work' scope as default.
            //ABP automatically saves all changes when a 'unit of work' scope ends (without any exception).
        }

        public void CreateMeeting(CreateMeetingInput input, string accessToken)
        {
            //We can use Logger, it's defined in ApplicationService class.
            Logger.Info("Creating a meeting for input: " + input);

            //Creating a new Meeting entity with given input's properties
            var meeting = new Meeting
            {
                TopicName = input.topic_name,
                MeetingPass = input.password,
                Description = input.agenda,
                StartTime = DateTime.Parse(input.start_time),
                EndTime = DateTime.Parse(input.end_date_time)
            };

            if (input.AssignedPersonId.HasValue)
                meeting.AssignedPersonId = input.AssignedPersonId.Value;

            var client = new RestClient("https://api.zoom.us/v2/users/" + Const.userId + "/meetings");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Bearer " + accessToken);

            string PostData = new JavaScriptSerializer().Serialize(new
            {
                topic = input.topic_name,
                type = 2,
                input.start_time,
                input.duration,
                schedule_for = "",
                timezone = "Asia/Saigon",
                input.password,
                input.agenda,
                recurrence = new
                {
                    type = 1,
                    repeat_interval = 1,
                    weekly_days = 1,
                    monthly_day = 1,
                    monthly_week = 1,
                    monthly_week_day = 1,
                    end_times = 1,
                    input.end_date_time,

                },
                setting = new
                {
                    host_video = false,
                    participant_video = false,
                    cn_meeting = false,
                    in_meeting = false,
                    join_before_host = true,
                    mute_upon_entry = false,
                    watermark = false,
                    usePmi = false,
                    approval_type = 2,
                    registration_type = 1,
                    audio = "both",
                    auto_recording = "none",
                    enforce_login = "",
                    enforce_login_domains = "",
                    alternative_hosts = "",
                    global_dial_in_countries = new string[]
                    {
                        "",
                    },
                    registrants_email_notification = false
                },
            });

            request.AddParameter("application/json", PostData, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var zoomToken = new ZoomToken(Const.apiKey, Const.apiSecret);
                string newJWTToken = zoomToken.Token;
                accessToken = newJWTToken;
                request = new RestRequest(Method.POST);
                request.AddHeader("authorization", "Bearer " + accessToken);
                request.AddParameter("application/json", "", ParameterType.RequestBody);

                response = client.Execute(request);
            }

            if (response.StatusCode == HttpStatusCode.Created)
            {
                dynamic data = JObject.Parse(response.Content);
                meeting.MeetingPass = data.password;
                meeting.Duration = input.duration;
                meeting.AccessToken = accessToken;
                meeting.MeetingId = data.id;

                //Saving entity with standard Insert method of repositories.
                _meetingRepository.Insert(meeting);
            }                         
        }

        public void Delete(EntityDto<long> input)
        {
            var meeting = _meetingRepository.GetMeetingById(input.Id);

            //Delete meeting in zoom server
            var client = new RestClient("https://api.zoom.us/v2/meetings/" + meeting.MeetingId);
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Bearer " + meeting.AccessToken);
            request.AddParameter("application/json", "", ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var zoomToken = new ZoomToken(Const.apiKey, Const.apiSecret);
                string newJWTToken = zoomToken.Token;
                request = new RestRequest(Method.DELETE);
                request.AddHeader("authorization", "Bearer " + newJWTToken);
                request.AddParameter("application/json", "", ParameterType.RequestBody);

                response = client.Execute(request);                
            }
                          
            _meetingRepository.Delete(meeting);
        }        

        public string GetMeetingInfor(long meetingId, string accessToken)
        {
            try
            {
                string result = "";

                var client = new RestClient("https://api.zoom.us/v2/meetings/" + meetingId);
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("authorization", "Bearer " + accessToken);
                request.AddParameter("application/json", "", ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var zoomToken = new ZoomToken(Const.apiKey, Const.apiSecret);
                    string newJWTToken = zoomToken.Token;
                    request = new RestRequest(Method.GET);
                    request.AddHeader("authorization", "Bearer " + newJWTToken);
                    request.AddParameter("application/json", "", ParameterType.RequestBody);

                    response = client.Execute(request);
                }

                result = response.Content.ToString();

                return result;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
