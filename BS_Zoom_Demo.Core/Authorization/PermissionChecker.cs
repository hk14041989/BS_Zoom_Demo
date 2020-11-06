using Abp.Authorization;
using BS_Zoom_Demo.Authorization.Roles;
using BS_Zoom_Demo.Authorization.Users;

namespace BS_Zoom_Demo.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
