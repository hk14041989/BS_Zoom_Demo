using System.Collections.Generic;
using BS_Zoom_Demo.Roles.Dto;

namespace BS_Zoom_Demo.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }

        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}