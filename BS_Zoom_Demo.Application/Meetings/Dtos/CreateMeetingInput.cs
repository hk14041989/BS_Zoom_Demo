using System.ComponentModel.DataAnnotations;

namespace BS_Zoom_Demo.Meetings.Dtos
{
    public class CreateMeetingInput
    {
        public int? AssignedPersonId { get; set; }

        [Required]
        public string topic_name { get; set; }

        public string agenda { get; set; }

        public string password { get; set; }

        public string start_time { get; set; }

        public string end_date_time { get; set; }

        public int duration { get; set; }

        public override string ToString()
        {
            return string.Format("[CreateMeetingInput > AssignedPersonId = {0}, Description = {1}]", AssignedPersonId, agenda);
        }
    }
}
