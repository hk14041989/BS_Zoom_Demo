using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using BS_Zoom_Demo.Roles.Dto;
using BS_Zoom_Demo.Users.Dto;

namespace BS_Zoom_Demo.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}