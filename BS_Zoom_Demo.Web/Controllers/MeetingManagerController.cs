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
        private readonly ILookupAppService _lookupAppService;
        private readonly string JWTToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJhdWQiOm51bGwsImlzcyI6InZTX0U0c0szUzd1RkVKazZJNzZLcnciLCJleHAiOjE2MDUyNjEwMzksImlhdCI6MTYwNTI1NTYzOX0.Mibp_6rQTRkTogCT1R5_qOsUtAhB-XBDOOThxpg7yvc";
        public List<SelectListItem> teachersSelectListItems = new List<SelectListItem>();

        public MeetingManagerController(
            IMeetingAppService meetingAppService, ILookupAppService lookupAppService)
        {
            _meetingAppService = meetingAppService;
            _lookupAppService = lookupAppService;            
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
    }
}