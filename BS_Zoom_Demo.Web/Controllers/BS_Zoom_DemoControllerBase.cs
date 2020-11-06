using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web.Mvc;

namespace BS_Zoom_Demo.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class BS_Zoom_DemoControllerBase : AbpController
    {
        protected BS_Zoom_DemoControllerBase()
        {
            LocalizationSourceName = BS_Zoom_DemoConsts.LocalizationSourceName;
        }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L("FormIsNotValidMessage"));
            }
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        #region JsonResult Response
        protected JsonResult JsonResult(int status, object data, string message)
        {
            dynamic result = new { status, data, message };
            JsonResult resultOut = Json(result, JsonRequestBehavior.AllowGet);
            resultOut.MaxJsonLength = Int32.MaxValue;
            return resultOut;
        }

        protected JsonResult BadRequest(string message, object data = null)
        {
            return JsonResult(400, data, message);
        }

        protected JsonResult BadRequest(IList<string> errorMessages)
        {
            if (errorMessages == null || errorMessages.Count == 0)
                return OK();

            string errorMessage = string.Join("<br />", errorMessages);
            return JsonResult(400, null, errorMessage);
        }

        protected JsonResult BadRequest(ModelStateDictionary modelState)
        {
            IList<string> errorMessages = new List<string>();
            foreach (ModelState state in modelState.Values)
            {
                foreach (ModelError error in state.Errors)
                {
                    errorMessages.Add(error.ErrorMessage);
                }
            }

            return BadRequest(string.Join(", ", errorMessages));
        }

        protected JsonResult Created(object data = null)
        {
            return OK(data, "Done");
        }

        protected JsonResult Deleted(string mess, object data = null)
        {
            return OK(data, "Deleted");
        }

        protected JsonResult Forbidden()
        {
            return JsonResult(403, null, "Access Denied");
        }

        protected JsonResult InternalServerError(string errorMessage)
        {
            return JsonResult(500, null, errorMessage);
        }
        protected JsonResult InternalServerError(Exception exception)
        {
            string log = "";
            if (ConfigurationManager.AppSettings["ShowError"] == "Off")
            {
                log = "";
                var message = "Has an error occurs";
#if DEBUG
                message += $":{exception?.Message}";
#endif
                return JsonResult(500, log, message);
            }
            else
            {
                log = exception.ToString();
                return JsonResult(500, log, log);
            }
        }

        protected JsonResult InvalidInput(ModelStateDictionary ModelState)
        {
            string errorMessage = "";
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    if (!string.IsNullOrEmpty(errorMessage))
                        errorMessage += "</br>";
                    errorMessage += error.ErrorMessage;
                }
            }
            if (string.IsNullOrEmpty(errorMessage))
            {
                errorMessage = "Input data is incorrect";
            }
            return BadRequest(errorMessage);
        }

        protected JsonResult NotFound()
        {
            return JsonResult(404, null, "Not Found");
        }

        protected JsonResult OK(object data = null, string message = null)
        {
            return JsonResult(200, data, message);
        }

        protected JsonResult Updated(object data = null)
        {
            return OK(data, "Updated");
        }

        protected JsonResult Copied(object data = null)
        {
            return OK(data, "Copied");
        }
        #endregion

        protected string ControlerAction
        {
            get
            {
                string result = "";
                var routeValues = HttpContext.Request.RequestContext.RouteData.Values;
                if (routeValues != null)
                {
                    result = routeValues["controller"].ToString() + "." + routeValues["action"].ToString();
                }
                return result;
            }
        }

        protected string PartialViewString(string viewName, object model = null)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }

        protected ActionResult ShowPartialView(string url, object vm)
        {
            ViewEngineResult result = ViewEngines.Engines.FindPartialView(this.ControllerContext, url);
            if (result.View != null)
                return PartialView(url, vm);
            else
                return BadRequest("The [X] page was not found.".Replace("[X]", "[" + Path.GetFileNameWithoutExtension(url) + "]"), Path.GetFileNameWithoutExtension(url));
        }
    }
}