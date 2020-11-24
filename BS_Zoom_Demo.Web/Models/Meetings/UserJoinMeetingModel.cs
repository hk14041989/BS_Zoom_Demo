using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BS_Zoom_Demo.Web.Models.Meetings
{
    public class UserJoinMeetingModel
    {
        public long meetingId { get; set; }
        public long userJoinMeetingId { get; set; }
    }
}