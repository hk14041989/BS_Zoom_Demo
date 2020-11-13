using Abp.Application.Services.Dto;
using System;

namespace BS_Zoom_Demo.Meetings.Dtos
{
    public class MeetingDto : EntityDto<long>
    {
        public int? AssignedPersonId { get; set; }

        public string TopicName { get; set; }

        public string AssignedPersonName { get; set; }

        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int Duration { get; set; }

        public MeetingState State { get; set; }

        public string AccessToken { get; set; }

        public string MeetingPass { get; set; }

        public long MeetingId { get; set; }

        //This method is just used by the Console Application to list tasks
        public override string ToString()
        {
            return string.Format(
                "[Meeting Id={0}, Description={1}, CreationTime={2}, AssignedPersonName={3}, State={4}]",
                Id,
                Description,
                CreationTime,
                AssignedPersonId,
                (MeetingState)State
                );
        }
    }
}
