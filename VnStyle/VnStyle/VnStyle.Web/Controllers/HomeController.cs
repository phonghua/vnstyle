using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ricky.Infrastructure.Core.Caching;
using Ricky.Infrastructure.Core.ObjectContainer;
using VnStyle.Services.Data;
using VnStyle.Services.Data.Domain;

namespace VnStyle.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICacheManager _cacheManager;
        private readonly IBaseRepository<Article> _postRepository;

        public HomeController()
        {
            _cacheManager = EngineContext.Current.Resolve<ICacheManager>();
            _postRepository = EngineContext.Current.Resolve<IBaseRepository<Article>>();
        }
        public ActionResult Index()
        {
            var posts = _postRepository.Table.ToList();

            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
