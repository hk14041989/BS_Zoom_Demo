using Abp.Web.Mvc.Views;

namespace BS_Zoom_Demo.Web.Views
{
    public abstract class BS_Zoom_DemoWebViewPageBase : BS_Zoom_DemoWebViewPageBase<dynamic>
    {

    }

    public abstract class BS_Zoom_DemoWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected BS_Zoom_DemoWebViewPageBase()
        {
            LocalizationSourceName = BS_Zoom_DemoConsts.LocalizationSourceName;
        }
    }
}