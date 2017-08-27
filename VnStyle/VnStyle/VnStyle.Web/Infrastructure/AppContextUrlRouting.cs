using System;
using System.Web;
using System.Web.Mvc;
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
        /// <summary>
        /// Get Salon Detail page url
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="salonName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string SalonDetail(string hashId, string salonName)
        {
            //return UrlHelper.GenerateUrl("FE_default", "Detail", "Home", null, HttpContext.Current.Reques, null, false);
            var urlhelper = new UrlHelper(HttpContext.Current.Request.RequestContext);

            var path = urlhelper.Action("Detail", "Home", new { area = "FE", id = hashId, title = CommonHelper.FriendlyUrl(salonName) });
            return urlhelper.BaseUrl() + path?.TrimStart('/');
        }

        public string Home()
        {
            var urlhelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            return urlhelper.Action("Index", "Home");
        }
    }
}