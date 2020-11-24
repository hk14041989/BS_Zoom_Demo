using Abp.Domain.Repositories;

namespace BS_Zoom_Demo.UserJoinMeetings
{
    /// <summary>
    /// Defines a repository to perform database operations for <see cref="UserJoinMeeting"/> Entities.
    /// 
    /// Extends <see cref="IRepository{TEntity, TPrimaryKey}"/> to inherit base repository functionality. 
    /// </summary>
    public interface IUserJoinMeetingRepository : IRepository<UserJoinMeeting, long>
    {        
    }
}
