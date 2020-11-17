using BS_Zoom_Demo.Meetings.Dtos;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BS_Zoom_Demo.Web.Models.Meetings
{
    public class EditMeetingModalViewModel
    {
        public MeetingDto Meet { get; set; }

        public List<SelectListItem> Teachers { get; set; }
    }
}