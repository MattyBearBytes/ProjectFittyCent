using System;
using System.Web;
using System.Web.Mvc;

namespace FittyCent.Web.Helpers
{
    public static class RouteHelper
    {
        public static bool IsCurrentAction(this HtmlHelper helper, string actionName, string controllerName)
        {
            var currentControllerName = (string)helper.ViewContext.RouteData.Values["controller"];
            var currentActionName = (string)helper.ViewContext.RouteData.Values["action"];

            return currentControllerName.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase) && currentActionName.Equals(actionName, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsCurrentArea(this HtmlHelper helper, string areaName)
        {
            var currentAreaName = (string)HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"] ?? string.Empty;

            return currentAreaName.Equals(areaName, StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsCurrentController(this HtmlHelper helper, string controllerName)
        {
            var currentControllerName = (string)helper.ViewContext.RouteData.Values["controller"];

            return currentControllerName.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}