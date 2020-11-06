using System.Collections.Generic;
using BS_Zoom_Demo.Roles.Dto;
using BS_Zoom_Demo.Users.Dto;

namespace BS_Zoom_Demo.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}