using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VnStyle.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region "Intro"
            routes.MapRoute(
                name: "Intro_Language",
                url: "{lang}/gioi-thieu",
                defaults: new { controller = "Home", action = "Intro", id = UrlParameter.Optional },
                constraints: new { lang = @"en" }
            );

            routes.MapRoute(
                name: "Intro",
                url: "gioi-thieu",
                defaults: new { controller = "Home", action = "Intro", id = UrlParameter.Optional, lang = "vi" }
            );
            #endregion

            #region "Intro"
            //routes.MapRoute(
            //    name: "Article",
            //    url: "{lang}/gioi-thieu",
            //    defaults: new { controller = "Home", action = "Intro", id = UrlParameter.Optional },
            //    constraints: new { lang = @"en" }
            //);

            routes.MapRoute(
                name: "Article",
                url: "{title}-{id}",
                defaults: new { controller = "Home", action = "Detail", lang = "vi" }

                //Detail(int id, string title = "")
            );
            #endregion

            #region "Default"
            routes.MapRoute(
                name: "Language",
                url: "{lang}/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { lang = @"en" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, lang = "vi" }
            );

            #endregion

        }
    }
}
