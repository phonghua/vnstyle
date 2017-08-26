using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ricky.Infrastructure.Core.Caching;
using Ricky.Infrastructure.Core.ObjectContainer;
using VnStyle.Services.Data.Domain;

namespace VnStyle.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICacheManager _cacheManager;

        public HomeController()
        {
            _cacheManager = EngineContext.Current.Resolve<ICacheManager>();
        }
        public ActionResult Index()
        {
            

            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
