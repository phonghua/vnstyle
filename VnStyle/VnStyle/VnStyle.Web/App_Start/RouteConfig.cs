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

            routes.RouteExistingFiles = true;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("PortalApp", "portal/{*url}", new { controller = "Portal", action = "Index" });


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

            

            #region "Tattoo"
            routes.MapRoute(
                name: "Tattoo_Language",
                url: "{lang}/tattoo",
                defaults: new { controller = "Home", action = "Tattoo", id = UrlParameter.Optional },
                constraints: new { lang = @"en" }
            );

            routes.MapRoute(
                name: "Tattoo",
                url: "tattoo",
                defaults: new { controller = "Home", action = "Tattoo", lang = "vi" }

            );
            #endregion


            #region "Piercing"
            routes.MapRoute(
                name: "Piercing_Language",
                url: "{lang}/piercing",
                defaults: new { controller = "Home", action = "Piercing", id = UrlParameter.Optional },
                constraints: new { lang = @"en" }
            );

            routes.MapRoute(
                name: "Piercing",
                url: "xo-khuyen",
                defaults: new { controller = "Home", action = "Piercing", lang = "vi" }

            );
            #endregion

            #region "Event"
            routes.MapRoute(
                name: "Event_Language",
                url: "{lang}/event",
                defaults: new { controller = "Home", action = "Events", id = UrlParameter.Optional },
                constraints: new { lang = @"en" }
            );

            routes.MapRoute(
                name: "Event",
                url: "su-kien",
                defaults: new { controller = "Home", action = "Events", lang = "vi" }

            );
            #endregion


            #region "Course"
            routes.MapRoute(
                name: "Course_Language",
                url: "{lang}/course",
                defaults: new { controller = "Home", action = "Course", id = UrlParameter.Optional },
                constraints: new { lang = @"en" }
            );

            routes.MapRoute(
                name: "Course",
                url: "khoa-hoc",
                defaults: new { controller = "Home", action = "Course", lang = "vi" }

            );
            #endregion

            #region "Search"
            routes.MapRoute(
                name: "SearchResult",
                url: "tim-kiem",
                defaults: new { controller = "Home", action = "Result", lang="vi" }
            );

            #endregion

            #region "Article"
            routes.MapRoute(
                name: "Article_Language",
                url: "{lang}/{title}-{id}",
                defaults: new { controller = "Home", action = "Detail", id = UrlParameter.Optional },
                constraints: new { lang = @"en" }
            );

            routes.MapRoute(
                name: "Article",
                url: "{title}-{id}",
                defaults: new { controller = "Home", action = "Detail", lang = "vi" }
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
