﻿using System;
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
using VnStyle.Services.Business;
using VnStyle.Services.Data;
using VnStyle.Services.Data.Domain;
using VnStyle.Services.Data.Enum;
using VnStyle.Web.Models.Home;
using VnStyle.Services.Business.Models;
using VnStyle.Web.Infrastructure.Helpers;
namespace VnStyle.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ICacheManager _cacheManager;
        private readonly IBaseRepository<Article> _articleRepository;
        
        private readonly IBaseRepository<ArticleLanguage> _articleLanguageRepository;
        private readonly IWorkContext _workContext;
        private readonly IResourceService _resourceService;
        private readonly IMediaService _mediaService;
        private readonly IArticleService _articleService;
        private readonly IArtistsService _artistsService;
        private readonly IVideoService _videoService;


        public HomeController()
        {
            _workContext = EngineContext.Current.Resolve<IWorkContext>();
            _cacheManager = EngineContext.Current.Resolve<ICacheManager>();
            _articleRepository = EngineContext.Current.Resolve<IBaseRepository<Article>>();            
            _articleLanguageRepository = EngineContext.Current.Resolve<IBaseRepository<ArticleLanguage>>();
            _resourceService = EngineContext.Current.Resolve<IResourceService>();
            _articleService = EngineContext.Current.Resolve<IArticleService>();
            _artistsService = EngineContext.Current.Resolve<IArtistsService>();
            _videoService = EngineContext.Current.Resolve<IVideoService>();
            _mediaService = EngineContext.Current.Resolve<IMediaService>();
        }
        public ActionResult Index()
        {

            var language = _workContext.CurrentLanguage;
            ViewBag.Title = "Home Page";
            

            return View();
        }
        public ActionResult Detail(int id, string title = "")
        {
            var article = _articleService.GetArticleById(id);
            if (article == null) return NotFound();

            // get SEO
            // get Related articles

            return View(article);
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult XamTv()
        {
            return View();
        }
        public ActionResult Result(string search = "am", int page = 1)
        {
            if (!String.IsNullOrEmpty(search))
            {
                var request = new GetArticlesRequest
                {
                    PageSize = 10,
                    PageIndex = page - 1
                };
                var result = _articleService.GetArticlesByString(search, request);
                if (result == null)
                {
                    return NotFound();
                }
                return View(result);
            }
            return View();
            
            
        }

        public ActionResult Piercing(int page = 1)
        {
            IPagedList<ArticleListingModel> result = GetArticleListing(page, ERootCategory.Piercing);
            return View(result);
        }

        public ActionResult Events(int page = 1)
        {
            IPagedList<ArticleListingModel> result = GetArticleListing(page, ERootCategory.Event);
            return View(result);
        }
        public ActionResult Course(int page = 1)
        {
            IPagedList<ArticleListingModel> result = GetArticleListing(page, ERootCategory.Course);
            return View(result);
        }

        private IPagedList<ArticleListingModel> GetArticleListing(int page, ERootCategory rootCate)
        {
            var request = new GetArticlesRequest
            {
                RootCate = (int)rootCate,
                PageSize = 10,
                PageIndex = page - 1
            };
            
            var result = _articleService.GetArticles(request);
            
            return result;
        }

        public ActionResult Tattoo(int page = 1)
        {
          
            IPagedList<ArticleListingModel> result = GetArticleListing(page, ERootCategory.Tattoo);
            return View(result);
        }

        public ActionResult Images(int id)
        {
            var model = _artistsService.GetAllImageByArtist(id);
            return View(model);
        }     

        public ActionResult Intro()
        {           
            var article = _articleService.GetArticleIntro();
            if (article == null) return NotFound();
            return View(article);
        }

        #region "Partial"

        [ChildActionOnly]
        public ActionResult GetVideos()
        {
            var videoThumb = _videoService.GetVideoThumb();
            return PartialView(videoThumb);
        }

        [ChildActionOnly]
        public ActionResult ArticleViewer(ArticleDetailModel model)
        {
            model.ArticleUrl = Url.CurrentUrl();
            return PartialView(model);
        }
        

        [ChildActionOnly]
        public ActionResult GetNewArticles(int page = 1)
        {
            var request = new GetArticlesRequest
            {
                PageSize = 10,
                PageIndex = page - 1
            };
            var model = _articleService.GetNewArticles(request);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult GetAllArtist()
        {

            var model = _artistsService.GetAllArtists();
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult GetArtistInMenu()
        {

            var model = _artistsService.GetAllArtists();
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult GetArticlesSession1()
        {
            var flag = true;
            var model = _articleService.GetSession(flag);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult GetArticlesSession2()
        {
            var flag = false;
            var model = _articleService.GetSession(flag);
            return PartialView(model);
        }


        [ChildActionOnly]
        public ActionResult SideBar1()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult SideBar2()
        {
            return PartialView();
        }



        [ChildActionOnly]
        public ActionResult Menu()
        {
            ViewBag.Active = "current-menu-item";
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            return PartialView();
        }


        [ChildActionOnly]
        public ActionResult HomePageFeaturedArticles()
        {
            var articles = _articleService.GetAllHomePageFeaturedArticles();
            return PartialView(articles);
        }


        
       

        #endregion

    }
}
