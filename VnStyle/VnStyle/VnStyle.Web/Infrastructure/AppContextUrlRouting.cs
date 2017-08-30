using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ricky.Infrastructure.Core;
using VnStyle.Services.Business;
using VnStyle.Web.Infrastructure.Helpers;

namespace VnStyle.Web.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class AppContextUrlRouting : IAppContextUrlRouting
    {


        public AppContextUrlRouting()
        {

        }
        /// <summary>
        /// Get Salon Detail page url
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="salonName"></param>
        /// <returns></returns>
        public string SalonDetail(string hashId, string salonName)
        {
            var urlhelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

            var path = urlhelper.Action("Detail", "Home", new { area = "FE", id = hashId, title = CommonHelper.FriendlyUrl(salonName) });
            return urlhelper.BaseUrl() + path?.TrimStart('/');
        }

        public string Home()
        {
            var urlhelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            return urlhelper.BaseUrl() + urlhelper.Action("Index", "Home");
        }

        //public string SwitchLanguage(string language)
        //{
        //    var urlhelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
        //    urlhelper.HostAction()
        //}

        //public string Url(string actionName, RouteData routeData)
        //{
            
        //    var urlhelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
        //    return urlhelper.Action(actionName, routeData);
        //}

        //public string Url(string actionName, string controllerName)
        //{
        //    //var routeValueDictionary = new RouteValueDictionary(routeData.Values);
        //    //if (routeValueDictionary.ContainsKey("lang"))
        //    //{
        //    //    if (routeData.Values["lang"] as string == lang)
        //    //    {
        //    //        liTagBuilder.AddCssClass("active");
        //    //    }
        //    //    else
        //    //    {
        //    //        routeValueDictionary["lang"] = lang;
        //    //    }
        //    //}
        //}

        

    }
}