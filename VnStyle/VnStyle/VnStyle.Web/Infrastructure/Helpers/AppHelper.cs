using System;
using System.Web;
using System.Web.Mvc;

namespace VnStyle.Web.Infrastructure.Helpers
{
    public static class AppHelper
    {
        public static string GetHost()
        {
            var httpContext = HttpContext.Current;
            var uri = httpContext.Request.Url;
            var host = uri.GetLeftPart(UriPartial.Authority);
            return host;
        }

        public static string HostAction(this UrlHelper url, string actionName, string controllerName, object routeValues)
        {
            return GetHost() + url.Action(actionName, controllerName, routeValues);
        }

        public static string HostContent(this UrlHelper url, string contentPath)
        {
            return GetHost() + url.Content(contentPath);
        }

        public static string Version()
        {
            return Guid.NewGuid().ToString();
        }

        //public static  string 
    }
}
