using Abp.EntityFramework;
using BS_Zoom_Demo.Meetings;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BS_Zoom_Demo.EntityFramework.Repositories
{
    public class MeetingRepository : BS_Zoom_DemoRepositoryBase<Meeting, long>, IMeetingRepository
    {
        public MeetingRepository(IDbContextProvider<BS_Zoom_DemoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public List<Meeting> GetAllWithTeachers(int? assignedPersonId, MeetingState? state)
        {
            //In repository methods, we do not deal with create/dispose DB connections, DbContexes and transactions. ABP handles it.

            var query = GetAll(); //GetAll() returns IQueryable<T>, so we can query over it.
            //var query = Context.Tasks.AsQueryable(); //Alternatively, we can directly use EF's DbContext object.
            //var query = Table.AsQueryable(); //Another alternative: We can directly use 'Table' property instead of 'Context.Tasks', they are identical.

            //Add some Where conditions...

            if (assignedPersonId.HasValue)
            {
                query = query.Where(meeting => meeting.AssignedPerson.Id == assignedPersonId.Value);
            }

            if (state.HasValue)
            {
                query = query.Where(meeting => meeting.State == state);
            }

            return query
                .OrderByDescending(meeting => meeting.CreationTime)
                .Include(meeting => meeting.AssignedPerson) //Include assigned person in a single query
                .ToList();
        }
    }
}
