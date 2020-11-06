using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using BS_Zoom_Demo.EntityFramework;

namespace BS_Zoom_Demo.Migrator
{
    [DependsOn(typeof(BS_Zoom_DemoDataModule))]
    public class BS_Zoom_DemoMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<BS_Zoom_DemoDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}