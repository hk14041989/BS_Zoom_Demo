using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using BS_Zoom_Demo.Teachers;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BS_Zoom_Demo.Meetings
{
    [Table("StsMeetings")]
    public class Meeting : Entity<long>, IHasCreationTime
    {
        public const int MaxTitleLength = 256;
        public const int MaxDescriptionLength = 64 * 1024; //64KB

        /// <summary>
        /// A reference (navigation property) to assigned <see cref="Person"/> for this task.
        /// We declare <see cref="ForeignKeyAttribute"/> for EntityFramework here. No need for NHibernate.
        /// </summary>
        [ForeignKey("AssignedPersonId")]
        public virtual Person AssignedPerson { get; set; }

        /// <summary>
        /// Database field for AssignedPerson reference.
        /// Needed for EntityFramework, no need for NHibernate.
        /// </summary>
        public virtual int? AssignedPersonId { get; set; }

        /// <summary>
        /// Describes the task.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// The time when this task is created.
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// Current state of the task.
        /// </summary>
        public virtual MeetingState State { get; set; }

        /// <summary>
        /// Default costructor.
        /// Assigns some default values to properties.
        /// </summary>
        public Meeting()
        {
            CreationTime = DateTime.Now;
            State = MeetingState.Active;
        }
    }
}
