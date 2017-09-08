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
using VnStyle.Services.Business;
using VnStyle.Services.Data;
using VnStyle.Services.Data.Domain;
using VnStyle.Services.Data.Enum;
using VnStyle.Web.Models.Home;

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
       

        public HomeController()
        {
            _workContext = EngineContext.Current.Resolve<IWorkContext>();
            _cacheManager = EngineContext.Current.Resolve<ICacheManager>();
            _articleRepository = EngineContext.Current.Resolve<IBaseRepository<Article>>();
            _articleLanguageRepository = EngineContext.Current.Resolve<IBaseRepository<ArticleLanguage>>();
            _resourceService = EngineContext.Current.Resolve<IResourceService>();
            
            _mediaService = EngineContext.Current.Resolve<IMediaService>();
        }
        public ActionResult Index()
        {

            var language = _workContext.CurrentLanguage;
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult Detail(int? id,string rootCate)
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
            var currentLanguage = _workContext.CurrentLanguage;
            var defaultLanguage = _resourceService.DefaultLanguageId();

            var articles = (from a in _articleRepository.Table.Where(p => p.RootCate == (int)ERootCategory.Event)
                            join al in _articleLanguageRepository.Table.Where(p => p.LanguageId == currentLanguage) on a.Id equals al.ArticleId
                            select new { a.Id, a.FeatureImageId, al.Content, al.HeadLine, al.Extract, a.CreatedDate }).ToList();

            
            var query = articles.Select(p => new ArticleViewerModelView
            {
                Id = p.Id,
                Content = p.Content,
                Headline = p.HeadLine,
                UrlImage = _mediaService.GetPictureUrl((long)p.FeatureImageId),
                Extract = p.Extract,
                CreatedDate = p.CreatedDate


            }).ToList();



            return View(query);

            
        }
        public ActionResult Course()
        {
            var currentLanguage = _workContext.CurrentLanguage;
            var defaultLanguage = _resourceService.DefaultLanguageId();

            var articles = (from a in _articleRepository.Table.Where(p => p.RootCate == (int)ERootCategory.Course)
                           join al in _articleLanguageRepository.Table.Where(p => p.LanguageId == currentLanguage) on a.Id equals al.ArticleId
                           select new {a.Id , a.FeatureImageId , al.Content,al.HeadLine,al.Extract, a.CreatedDate}).ToList();

            if (articles == null && currentLanguage == defaultLanguage)
            {
                //... return NOT FOUND
            }            

            
            var query = articles.Select(p => new ArticleViewerModelView
            {
                Id = p.Id,
                Content = p.Content,
                Headline = p.HeadLine,
                UrlImage = _mediaService.GetPictureUrl((long)p.FeatureImageId),
                Extract = p.Extract,
                CreatedDate = p.CreatedDate
               

            }).ToList();         
            
            
            
            return View(query);
            



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
            var currentLanguage = _workContext.CurrentLanguage;
            var defaultLanguage = _resourceService.DefaultLanguageId();

            //var article = from a in _articleRepository.Table.Where(p => p.RootCate == (int)ERootCategory.Intro)
            //              join al in _articleLanguageRepository.Table.Where(p => p.LanguageId == currentLanguage) on a.Id equals al.ArticleId
            //              select new { a, al };

            var articleLanguage = _articleLanguageRepository.Table.Where(p => p.Article.RootCate == (int)ERootCategory.Intro).FirstOrDefault(p => p.LanguageId == currentLanguage);
            if (articleLanguage == null && currentLanguage == defaultLanguage)
            {
                //... return NOT FOUND
            }
            articleLanguage = _articleLanguageRepository.Table.Where(p => p.Article.RootCate == (int)ERootCategory.Intro).FirstOrDefault(p => p.LanguageId == defaultLanguage);
            
            if (articleLanguage == null)
            {
                //... return NOT FOUND
            }
            
            var model = new ArticleViewerModelView
            {
                Headline = articleLanguage.HeadLine,
                Content = articleLanguage.Content
                
            };
            return View(model);
        }


        [ChildActionOnly]
        public ActionResult ArticleViewer(ArticleViewerModelView model)
        {
            return PartialView(model);
        }
    }
}
