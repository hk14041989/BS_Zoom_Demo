using BS_Zoom_Demo.EntityFramework;
using EntityFramework.DynamicFilters;

namespace BS_Zoom_Demo.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly BS_Zoom_DemoDbContext _context;

        public InitialHostDbBuilder(BS_Zoom_DemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
