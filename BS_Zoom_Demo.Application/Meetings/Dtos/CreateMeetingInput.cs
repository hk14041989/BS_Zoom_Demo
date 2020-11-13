using System.ComponentModel.DataAnnotations;

namespace BS_Zoom_Demo.Meetings.Dtos
{
    public class CreateMeetingInput
    {
        public int? AssignedPersonId { get; set; }

        [Required]
        public string Description { get; set; }

        public override string ToString()
        {
            return string.Format("[CreateTaskInput > AssignedPersonId = {0}, Description = {1}]", AssignedPersonId, Description);
        }
    }
}
