using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS_Zoom_Demo.Meetings
{
    /// <summary>
    /// Defines a repository to perform database operations for <see cref="Meeting"/> Entities.
    /// 
    /// Extends <see cref="IRepository{TEntity, TPrimaryKey}"/> to inherit base repository functionality. 
    /// </summary>
    public interface IMeetingRepository : IRepository<Meeting, long>
    {
        /// <summary>
        /// Gets all tasks with <see cref="Meeting.AssignedPerson"/> is retrived (Include for EntityFramework, Fetch for NHibernate)
        /// filtered by given conditions.
        /// </summary>
        /// <param name="assignedPersonId">Optional assigned person filter. If it's null, not filtered.</param>
        /// <param name="state">Optional state filter. If it's null, not filtered.</param>
        /// <returns>List of found tasks</returns>
        List<Meeting> GetAllWithTeachers(int? assignedPersonId, MeetingState? state);
    }
}
