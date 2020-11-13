using System.Collections.Generic;
using System.Web.Mvc;

namespace BS_Zoom_Demo.Web.Models.Teachers
{
    public class CreateMeetingViewModel
    {
        public List<SelectListItem> Teachers { get; set; }

        public CreateMeetingViewModel(List<SelectListItem> teachers)
        {
            Teachers = teachers;
        }
    }
}