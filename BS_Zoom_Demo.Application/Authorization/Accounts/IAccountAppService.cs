using System.Threading.Tasks;
using Abp.Application.Services;
using BS_Zoom_Demo.Authorization.Accounts.Dto;

namespace BS_Zoom_Demo.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
