using System.Threading.Tasks;
using Abp.Application.Services;
using BS_Zoom_Demo.Configuration.Dto;

namespace BS_Zoom_Demo.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}