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


        public HomeController()
        {
            _workContext = EngineContext.Current.Resolve<IWorkContext>();
            _cacheManager = EngineContext.Current.Resolve<ICacheManager>();
            _articleRepository = EngineContext.Current.Resolve<IBaseRepository<Article>>();
            _articleLanguageRepository = EngineContext.Current.Resolve<IBaseRepository<ArticleLanguage>>();
            _resourceService = EngineContext.Current.Resolve<IResourceService>();
            _articleService = EngineContext.Current.Resolve<IArticleService>();
            _mediaService = EngineContext.Current.Resolve<IMediaService>();
        }
        public ActionResult Index()
        {

            var language = _workContext.CurrentLanguage;
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Detail(int id)
        {

            //if (id == null)
            //{
            //    //
            //}
            //else
            //{
            //    var request = new ArticleModelRequest
            //    {

            //        currentLanguage = _workContext.CurrentLanguage,
            //        defaultLanguage = _resourceService.DefaultLanguageId()

            //    };
            //    var article = _articleService.GetArticleById(id, request);
            //    return View(article);
            //}


            //var request = new ArticleModelRequest
            //{

            //    currentLanguage = _workContext.CurrentLanguage,
            //    defaultLanguage = _resourceService.DefaultLanguageId()

            //};
            //var article = _articleService.GetArticleById(id, request);
            //if (article == null) return NotFound();

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
            var request = new ArticleModelRequest
            {
                rootCate = (int)ERootCategory.Event,
                currentLanguage = _workContext.CurrentLanguage,
                defaultLanguage = _resourceService.DefaultLanguageId()

            };

            var result = _articleService.GetArticles(request);


            return View(result);


        }
        public ActionResult Course()
        {
            var request = new ArticleModelRequest
            {
                rootCate = (int)ERootCategory.Course,
                currentLanguage = _workContext.CurrentLanguage,
                defaultLanguage = _resourceService.DefaultLanguageId()

            };

            var result = _articleService.GetArticles(request);


            return View(result);






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

        public ActionResult Footer()
        {
            return PartialView();
        }

        public ActionResult Intro()
        {
            var request = new ArticleModelRequest
            {
                rootCate = (int)ERootCategory.Intro,
                currentLanguage = _workContext.CurrentLanguage,
                defaultLanguage = _resourceService.DefaultLanguageId()

            };
            var article = _articleService.GetArticleIntro(request);
            if(article == null) return NotFound();
            return View(article);
        }

        

        [ChildActionOnly]
        public ActionResult ArticleViewer(ArticleModelView model)
        {
            return PartialView(model);
        }
    }
}
