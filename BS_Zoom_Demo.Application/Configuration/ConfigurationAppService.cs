using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using BS_Zoom_Demo.Configuration.Dto;

namespace BS_Zoom_Demo.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : BS_Zoom_DemoAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
