using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS_Zoom_Demo.Meetings.Dtos
{
    public class UpdateMeetingInput : ICustomValidate
    {
        [Range(1, long.MaxValue)] //Data annotation attributes work as expected.
        public long MeetingId { get; set; }

        public int? AssignedPersonId { get; set; }

        public MeetingState? State { get; set; }

        //Custom validation method. It's called by ABP after data annotation validations.
        public void AddValidationErrors(CustomValidationContext context)
        {
            if (AssignedPersonId == null && State == null)
            {
                context.Results.Add(new ValidationResult("Both of AssignedPersonId and State can not be null in order to update a Meeting!", new[] { "AssignedPersonId", "State" }));
            }
        }

        public override string ToString()
        {
            return string.Format("[UpdateMeetingInput > MeetingId = {0}, AssignedPersonId = {1}, State = {2}]", MeetingId, AssignedPersonId, State);
        }
    }
}
