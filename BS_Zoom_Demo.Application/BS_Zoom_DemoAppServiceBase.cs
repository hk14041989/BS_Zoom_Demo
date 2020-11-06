using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using BS_Zoom_Demo.Authorization.Users;
using BS_Zoom_Demo.MultiTenancy;
using BS_Zoom_Demo.Users;
using Microsoft.AspNet.Identity;

namespace BS_Zoom_Demo
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class BS_Zoom_DemoAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        protected BS_Zoom_DemoAppServiceBase()
        {
            LocalizationSourceName = BS_Zoom_DemoConsts.LocalizationSourceName;
        }

        protected virtual async Task<User> GetCurrentUserAsync()
        {
            var user = await UserManager.FindByIdAsync(AbpSession.GetUserId());
            if (user == null)
            {
                throw new ApplicationException("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}