using Abp.Application.Services.Dto;
using BS_Zoom_Demo.Common;
using BS_Zoom_Demo.Meetings;
using BS_Zoom_Demo.Meetings.Dtos;
using BS_Zoom_Demo.Web.Models.Meetings;
using BS_Zoom_Demo.Web.Models.Teachers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BS_Zoom_Demo.Web.Controllers
{
    public class MeetingManagerController : BS_Zoom_DemoControllerBase
    {
        private readonly IMeetingAppService _meetingAppService;
        private readonly IMeetingRepository _meetingRepository;
        private readonly ILookupAppService _lookupAppService;
        private readonly string JWTToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJhdWQiOm51bGwsImlzcyI6InZTX0U0c0szUzd1RkVKazZJNzZLcnciLCJleHAiOjE2MDYwOTQ1NjUsImlhdCI6MTYwNTQ4OTc3MX0.ShXotWc-n6Hvm1-FHHpkp4Qf8uNWS97kJVDY5p72q5A";
        public List<SelectListItem> teachersSelectListItems = new List<SelectListItem>();

        public MeetingManagerController(
            IMeetingAppService meetingAppService, ILookupAppService lookupAppService, IMeetingRepository meetingRepository)
        {
            _meetingAppService = meetingAppService;
            _lookupAppService = lookupAppService;
            _meetingRepository = meetingRepository;
        }


        // GET: MeetingManager
        public ActionResult Index(GetMeetingsInput input)
        {
            var output = _meetingAppService.GetMeetings(input);

            teachersSelectListItems = _lookupAppService.GetTeachersComboboxItems().Items
                .Select(p => p.ToSelectListItem())
                .ToList();

            teachersSelectListItems.Insert(0, new SelectListItem { Value = string.Empty, Text = L("Unassigned"), Selected = true });

            var model = new IndexViewModel(output.Meetings, LocalizationManager, JWTToken, teachersSelectListItems)
            {
                SelectedMeetingState = input.State
            };

            return View(model);
        }

        public ActionResult Create()
        {
            return View(new CreateMeetingViewModel(teachersSelectListItems, JWTToken));
        }

        public ActionResult EditMeetingModal(long meetingId)
        {
            var meet = _meetingRepository.GetMeetingById(meetingId);
            var model = new EditMeetingModalViewModel
            {
                Meet = new MeetingDto()
                {
                    Id = meet.Id,
                    MeetingId = meet.MeetingId,
                    MeetingPass = meet.MeetingPass,
                    StartTime = meet.StartTime,
                    State = meet.State,
                    TopicName = meet.TopicName,
                    AccessToken = meet.AccessToken,
                    AssignedPersonId = meet.AssignedPerson != null ? meet.AssignedPerson.Id : 0,
                    AssignedPersonName = meet.AssignedPerson != null ? meet.AssignedPerson.Name : "",
                    CreationTime = meet.CreationTime,
                    Description = meet.Description,
                    Duration = meet.Duration,
                    EndTime = meet.EndTime
                }
            };

            teachersSelectListItems = _lookupAppService.GetTeachersComboboxItems().Items
                .Select(p => p.ToSelectListItem())
                .ToList();

            teachersSelectListItems.Insert(0, new SelectListItem { Value = string.Empty, Text = L("Unassigned"), Selected = true });

            model.Teachers = teachersSelectListItems;

            return View("_EditMeetingModal", model);
        }
    }
}