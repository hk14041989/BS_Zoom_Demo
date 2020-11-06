using System.Collections.Generic;
using System.Linq;
using BS_Zoom_Demo.Roles.Dto;
using BS_Zoom_Demo.Users.Dto;

namespace BS_Zoom_Demo.Web.Models.Users
{
    public class EditUserModalViewModel
    {
        public UserDto User { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }

        public bool UserIsInRole(RoleDto role)
        {
            return User.Roles != null && User.Roles.Any(r => r == role.Name);
        }
    }
}