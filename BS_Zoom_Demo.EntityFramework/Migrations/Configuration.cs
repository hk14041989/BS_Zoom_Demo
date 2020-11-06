using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using BS_Zoom_Demo.Migrations.SeedData;
using EntityFramework.DynamicFilters;

namespace BS_Zoom_Demo.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<BS_Zoom_Demo.EntityFramework.BS_Zoom_DemoDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "BS_Zoom_Demo";
        }

        protected override void Seed(BS_Zoom_Demo.EntityFramework.BS_Zoom_DemoDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}
