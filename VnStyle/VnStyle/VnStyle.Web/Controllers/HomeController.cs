﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
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
using VnStyle.Services.Business.Settings;
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
        private readonly ISettingService _settingService;


        public HomeController()
        {
            _settingService = EngineContext.Current.Resolve<ISettingService>();
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
            var modelView = new IndexModelView
            {
                MetaTag = new SiteMetaTag
                {
                    CurrentUrl = Url.CurrentUrl(),
                    Description = "VNStyle Tattoo Studio cơ sở xăm uy tín với đội ngũ nghệ nhân chuyên nghiệp",
                    Image = Url.HostContent("~/Content/images/logo-big-dark.png"),
                    Keywords = "VNStyle Tattoo, xăm uy tín, xam uy tin, tha thu, tattoo",
                    Publisher = "Phong Hua Dai",
                    ContentCreatedDate = DateTime.Now
                }
            };
            //var a = _articleService.GetFeaturedFirst();
            return View(modelView);
        }
        public ActionResult Detail(int id, string title = "")
        {
            var article = _articleService.GetArticleById(id);
            if (article == null) return NotFound();
            article.UrlImage = Url.HostContent(article.UrlImage);

            // get SEO
            // get Related articles

            var appSetting = _settingService.LoadConfiguration<AppSetting>();

            var seoMetaTag = _articleService.GetMetaTagById(article.MetaTagId);
            var modelView = new ArticleViewerModelView
            {
                Article = article,
                MetaTag = new SiteMetaTag
                {
                    Description = seoMetaTag.Description,
                    Keywords = seoMetaTag.Keywords,
                    CurrentUrl = Url.CurrentUrl(),
                    Image = article.UrlImage
                }
            };
            return View(modelView);
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult XamTv()
        {
            return View();
        }

        public ActionResult Result(string search = "", int page = 1)
        {
            ViewBag.Key = search;
            var request = new GetArticlesRequest
            {
                PageSize = 10,
                PageIndex = page - 1
            };
            if (!String.IsNullOrEmpty(search))
            {

                var result = _articleService.GetArticlesByString(search, request);
                if (result == null)
                {
                    return NotFound();
                }
                return View(result);
            }
            else
            {
                var result = _articleService.GetNewArticles(request);
                return View(result);
            }



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
                PageSize = 5,
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


        [ChildActionOnly]
        public ActionResult GetNewArticles(int page = 1)
        {
            var request = new GetArticlesRequest
            {
                PageSize = 6,
                PageIndex = page - 1
            };
            var model = _articleService.GetNewArticles(request);
            return PartialView(model);
        }
        public ActionResult ArticleMore(int page)
        {
            var model = _articleService.GetNewArticles(new GetArticlesRequest
            {
                PageIndex = page - 1,
                PageSize = 6
            });
            return PartialView("ArticleMore", model);
        }
        public ActionResult VideoMore(int page)
        {
            var model = _videoService.GetVideoThumb(new GetArticlesRequest
            {
                PageIndex = page - 1,
                PageSize = 5
            });
            return PartialView("VideoMore", model);
        }
        public ActionResult DetailMovie(int id, string title = "")
        {
            var movie = _videoService.GetVideoById(id);
            return View(movie);
        }

        #region "Partial"

        [ChildActionOnly]
        public ActionResult GetVideosRelated()
        {

            var videoThumb = _videoService.GetRelatedVideo();
            //return PartialView(videoThumb);
            return PartialView(videoThumb);
        }

        [ChildActionOnly]
        public ActionResult GetVideos(int page = 1)
        {
            var request = new GetArticlesRequest
            {
                PageSize = 5,
                PageIndex = page - 1
            };
            var videoThumb = _videoService.GetVideoThumb(request);
            //return PartialView(videoThumb);
            return PartialView(videoThumb);
        }


        [ChildActionOnly]
        public ActionResult ArticleViewer(ArticleDetailModel model)
        {
            model.ArticleUrl = Url.CurrentUrl();
            return PartialView(model);
        }



        [ChildActionOnly]
        public ActionResult GetAllArtist()
        {

            var model = _artistsService.GetAllArtists();
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult GetImageArtistInMenu()
        {

            var model = _artistsService.GetImage();
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
            var model = _articleService.GetSession(1);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult GetArticlesSession2()
        {
            var model = _articleService.GetSession(2);
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
            var articles = _articleService.GetFirstHomePageFeaturedArticles();
            if (articles == null)
            {
                NotFound();
            }
            return PartialView(articles);
        }

        [ChildActionOnly]
        public ActionResult HomePageLastFeaturedArticles()
        {
            var articles = _articleService.GetLastHomePageFeaturedArticles();
            return PartialView(articles);
        }





        #endregion


        public ActionResult TestLayout()
        {
            return View();
        }
    }
}
