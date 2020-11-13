using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS_Zoom_Demo.Meetings.Dtos
{
    public class GetMeetingsInput
    {
        public MeetingState? State { get; set; }

        public int? AssignedPersonId { get; set; }
    }
}
