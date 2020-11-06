using System.Threading.Tasks;
using Abp.Application.Services;
using BS_Zoom_Demo.Sessions.Dto;

namespace BS_Zoom_Demo.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
