using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BS_Zoom_Demo.UserJoinMeetings
{
    [Table("StsUserJoinMeeting")]
    public class UserJoinMeeting : Entity<long>
    {
        public virtual string UserId { get; set; }

        public virtual string UserName { get; set; }

        public virtual DateTime? JoinTime { get; set; }

        public virtual string MeetingId { get; set; }

        public virtual int MeetingType { get; set; }

        public virtual DateTime? LeaveTime { get; set; }

        /// <summary>
        /// Default costructor.
        /// Assigns some default values to properties.
        /// </summary>
        public UserJoinMeeting()
        {
            JoinTime = DateTime.Now;
        }
    }
}
