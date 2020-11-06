using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using BS_Zoom_Demo.MultiTenancy;

namespace BS_Zoom_Demo.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}