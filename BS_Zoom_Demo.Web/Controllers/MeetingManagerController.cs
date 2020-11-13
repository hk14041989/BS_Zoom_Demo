using Abp.Application.Services.Dto;
using BS_Zoom_Demo.Common;
using BS_Zoom_Demo.Meetings;
using BS_Zoom_Demo.Meetings.Dtos;
using BS_Zoom_Demo.Web.Models.Meetings;
using BS_Zoom_Demo.Web.Models.Teachers;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BS_Zoom_Demo.Web.Controllers
{
    public class MeetingManagerController : BS_Zoom_DemoControllerBase
{
        private readonly IMeetingAppService _meetingAppService;
        private readonly ILookupAppService _lookupAppService;

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

            var model = new IndexViewModel(output.Meetings, LocalizationManager)
            {
                SelectedMeetingState = input.State
            };

            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            var teachersSelectListItems = (await _lookupAppService.GetTeachersComboboxItems()).Items
                .Select(p => p.ToSelectListItem())
                .ToList();

            teachersSelectListItems.Insert(0, new SelectListItem { Value = string.Empty, Text = L("Unassigned"), Selected = true });

            return View(new CreateMeetingViewModel(teachersSelectListItems));
        }
    }
}