using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BS_Zoom_Demo.MultiTenancy.Dto;

namespace BS_Zoom_Demo.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
