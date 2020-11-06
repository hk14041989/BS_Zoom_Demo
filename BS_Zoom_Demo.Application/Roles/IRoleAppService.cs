using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BS_Zoom_Demo.Roles.Dto;

namespace BS_Zoom_Demo.Roles
{
    public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedResultRequestDto, CreateRoleDto, RoleDto>
    {
        Task<ListResultDto<PermissionDto>> GetAllPermissions();
    }
}
