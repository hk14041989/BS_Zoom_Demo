using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using BS_Zoom_Demo.EntityFramework;

namespace BS_Zoom_Demo
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(BS_Zoom_DemoCoreModule))]
    public class BS_Zoom_DemoDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<BS_Zoom_DemoDbContext>());

            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
