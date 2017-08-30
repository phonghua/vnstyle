using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ricky.Infrastructure.Core.ObjectContainer;
using VnStyle.Services.Business;

namespace VnStyle.Web.Infrastructure.Helpers
{
    public static class AppHelper
    {
        public static string HostAction(this UrlHelper url, string actionName, string controllerName, object routeValues)
        {
            return url.BaseUrl() + url.Action(actionName, controllerName, routeValues);
        }

        public static string HostAction(this UrlHelper url, string actionName)
        {
            return url.BaseUrl() + url.Action(actionName);
        }

        public static string HostAction(this UrlHelper url, string actionName, string controllerName)
        {
            return url.BaseUrl() + url.Action(actionName, controllerName);
        }

        public static string Language(this UrlHelper url, string lang)
        {
            var resourceService = EngineContext.Current.Resolve<IResourceService>();
            var isDefault = resourceService.GetLanguages().Any(p => p.IsDefault && p.Code == lang);

            var routeValueDictionary = new RouteValueDictionary(url.RequestContext.RouteData.Values);
            if (routeValueDictionary.ContainsKey("lang")) routeValueDictionary.Remove("lang");
            routeValueDictionary["lang"] = lang;

            return url.BaseUrl() + url.RouteUrl(routeValueDictionary);
        }

        public static string HostContent(this UrlHelper url, string contentPath)
        {
            return url.BaseUrl() + url.Content(contentPath);
        }

        public static string Version()
        {
            return Guid.NewGuid().ToString();
        }

        public static string T(this HtmlHelper html, string text, params object[] args)
        {
            return EngineContext.Current.Resolve<IResourceService>().T(text, args);
        }
    }
}
