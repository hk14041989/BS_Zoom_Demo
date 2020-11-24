using Abp.EntityFramework;
using BS_Zoom_Demo.UserJoinMeetings;

namespace BS_Zoom_Demo.EntityFramework.Repositories
{
    public class UserJoinMeetingRepository : BS_Zoom_DemoRepositoryBase<UserJoinMeeting, long>, IUserJoinMeetingRepository
    {
        public UserJoinMeetingRepository(IDbContextProvider<BS_Zoom_DemoDbContext> dbContextProvider)
        : base(dbContextProvider)
        {
        }


    }
}
