using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace BS_Zoom_Demo.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : BS_Zoom_DemoControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}