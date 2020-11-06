using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using BS_Zoom_Demo.Authorization.Users;
using BS_Zoom_Demo.Editions;

namespace BS_Zoom_Demo.MultiTenancy
{
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        public TenantManager(
            IRepository<Tenant> tenantRepository, 
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository, 
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore
            ) 
            : base(
                tenantRepository, 
                tenantFeatureRepository, 
                editionManager,
                featureValueStore
            )
        {
        }
    }
}