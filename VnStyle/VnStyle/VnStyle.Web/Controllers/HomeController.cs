using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Ricky.Infrastructure.Core;
using Ricky.Infrastructure.Core.Caching;
using Ricky.Infrastructure.Core.ObjectContainer;
using VnStyle.Services.Data;
using VnStyle.Services.Data.Domain;

namespace VnStyle.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ICacheManager _cacheManager;
        private readonly IBaseRepository<Article> _postRepository;
        private readonly IWorkContext _workContext;

        public HomeController()
        {
            _workContext = EngineContext.Current.Resolve<IWorkContext>();
            _cacheManager = EngineContext.Current.Resolve<ICacheManager>();
            _postRepository = EngineContext.Current.Resolve<IBaseRepository<Article>>();
        }
        public ActionResult Index()
        {
            


            
            

            var language = _workContext.CurrentLanguage;
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Detail()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult XamTv()
        {
            return View();
        }
        public ActionResult Events()
        {
            return View();
        }
        public ActionResult Courses()
        {
            return View();
        }
        public ActionResult Images()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            return PartialView();
        }

    }
}
