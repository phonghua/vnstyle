using System.Web.Mvc;

namespace VnStyle.Web.Infrastructure.Helpers
{
    public static class UrlHelpers
    {
        
        public static string BaseUrl(this UrlHelper url)
        {
            var request = url.RequestContext.HttpContext.Request;
            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, url.Content("~"));

            return baseUrl.TrimEnd('/');
        }

        public static string Hashtag(this UrlHelper url, string hashTag)
        {
            var request = url.RequestContext.HttpContext.Request;
            var currentUrl = request.Url.AbsoluteUri;
            if (currentUrl.EndsWith("/")) currentUrl = currentUrl.TrimEnd('/');
            if (currentUrl.EndsWith("#")) currentUrl = currentUrl.TrimEnd('#');
            if (currentUrl.EndsWith("/")) currentUrl = currentUrl.TrimEnd('/');
            return currentUrl + "/#/" + hashTag;
        }

        public static string Hashtag(this UrlHelper url, string actionName, string controllerName, object route, string hashtag)
        {
            return url.Action(actionName, controllerName, route) + "/#/" + hashtag;
        }

        
    }

    
}