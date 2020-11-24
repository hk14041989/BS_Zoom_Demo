namespace BS_Zoom_Demo.Meetings.Dtos
{
    public class GetMeetingsInput
    {
        public MeetingState? State { get; set; }

        public int? AssignedPersonId { get; set; }
    }
}
