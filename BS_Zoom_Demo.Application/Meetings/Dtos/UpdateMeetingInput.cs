using Abp.Runtime.Validation;
using System.ComponentModel.DataAnnotations;

namespace BS_Zoom_Demo.Meetings.Dtos
{
    public class UpdateMeetingInput : ICustomValidate
    {
        [Range(1, long.MaxValue)] //Data annotation attributes work as expected.
        public long MeetingId { get; set; }

        public int? AssignedPersonId { get; set; }

        public MeetingState? State { get; set; }

        public string topic_name { get; set; }

        public string start_time { get; set; }

        public string end_date_time { get; set; }

        public int duration { get; set; }

        public string meetting_password { get; set; }

        public string agenda { get; set; }

        //Custom validation method. It's called by ABP after data annotation validations.
        public void AddValidationErrors(CustomValidationContext context)
        {
            if (string.IsNullOrEmpty(topic_name) && string.IsNullOrEmpty(start_time) && string.IsNullOrEmpty(end_date_time) && string.IsNullOrEmpty(meetting_password) 
                && duration == null)
                context.Results.Add(new ValidationResult("Topic Name, Start Time, End Time and Duration can not be null in order to update a Meeting!", new[] { "AssignedPersonId", "State" }));
        }

        public override string ToString()
        {
            return string.Format("[UpdateMeetingInput > MeetingId = {0}, AssignedPersonId = {1}, State = {2}]", MeetingId, AssignedPersonId, State);
        }
    }
}
