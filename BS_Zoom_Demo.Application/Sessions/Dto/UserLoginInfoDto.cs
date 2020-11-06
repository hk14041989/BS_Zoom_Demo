using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using BS_Zoom_Demo.Authorization.Users;
using BS_Zoom_Demo.Users;

namespace BS_Zoom_Demo.Sessions.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserLoginInfoDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }
    }
}
