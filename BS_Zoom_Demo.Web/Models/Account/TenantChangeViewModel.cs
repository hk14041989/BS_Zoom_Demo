using Abp.AutoMapper;
using BS_Zoom_Demo.Sessions.Dto;

namespace BS_Zoom_Demo.Web.Models.Account
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}