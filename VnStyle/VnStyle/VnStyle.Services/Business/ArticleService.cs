using Ricky.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VnStyle.Services.Business.Models;
using VnStyle.Services.Data;
using VnStyle.Services.Data.Domain;
using VnStyle.Services.Data.Enum;

namespace VnStyle.Services.Business
{
    public class ArticleService : IArticleService
    {
        #region
        private readonly IBaseRepository<Article> _articleRepository;
        private readonly IBaseRepository<ArticleLanguage> _articleLanguageRepository;
        private readonly IWorkContext _workContext;
        private readonly IResourceService _resourceService;
        private readonly IMediaService _mediaService;


        public ArticleService(IBaseRepository<Article> articleRepository, IBaseRepository<ArticleLanguage> articleLanguageRepositor, IWorkContext workContext, IResourceService resourceService, IMediaService mediaService)
        {
            _articleRepository = articleRepository;
            _articleLanguageRepository = articleLanguageRepositor;
            _workContext = workContext;
            _resourceService = resourceService;
            _mediaService = mediaService;
        }
        #endregion

        public IPagedList<ArticleListingModel> GetArticles(GetArticlesRequest request)
        {
            if (request.PageIndex < 0) request.PageIndex = 0;
            if (request.PageSize < 1) request.PageSize = 10;
            var currentLanguage = _workContext.CurrentLanguage;

            var articleQuery = (from a in _articleRepository.Table.Where(p => p.RootCate == request.RootCate && p.IsActive == true)
                                join al in _articleLanguageRepository.Table.Where(p => p.LanguageId == currentLanguage) on a.Id equals al.ArticleId
                                select new ArticleListingModel { Id = a.Id, ImageId = a.FeatureImageId, HeadLine = al.HeadLine, Extract = al.Extract, PushlishDate = a.PublishDate });






            var total = articleQuery.Count();
            var pagedArticles = articleQuery.OrderByDescending(p => p.PushlishDate).Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToList();

           
            foreach (var article in pagedArticles)
            {
                if (article.ImageId.HasValue)
                    article.UrlImage = _mediaService.GetPictureUrl(article.ImageId.Value);
                else
                    article.UrlImage = "~/Content/images/no-image.png";
            };
       

            return new PagedList<ArticleListingModel>(pagedArticles, request.PageIndex, request.PageSize, total);

        }

        public ArticleDetailModel GetArticleIntro()
        {
            var currentLanguage = _workContext.CurrentLanguage;
            var defaultLanguage = _resourceService.DefaultLanguageId();

            var articleLanguage = _articleLanguageRepository.Table.Where(p => p.Article.RootCate == (int)ERootCategory.Intro && p.Article.IsActive
            && p.LanguageId == currentLanguage).Select(p => new ArticleDetailModel
            {
                Id = p.ArticleId,
                HeadLine = p.HeadLine,
                Content = p.Content,
                ImageId = p.Article.FeatureImageId,
                Extract = p.Extract,
            }).FirstOrDefault();
            if (articleLanguage == null && currentLanguage == defaultLanguage)
            {
                return null;
            }

            articleLanguage = _articleLanguageRepository.Table.Where(p => p.Article.RootCate == (int)ERootCategory.Intro && p.Article.IsActive
            && p.LanguageId == defaultLanguage
            ).Select(p => new ArticleDetailModel
            {
                Id = p.ArticleId,
                HeadLine = p.HeadLine,
                Content = p.Content,
                ImageId = p.Article.FeatureImageId,
                Extract = p.Extract,
            }).FirstOrDefault();
            if (articleLanguage == null)
            {
                return null;
            }


            if (articleLanguage.ImageId.HasValue)
            {
                articleLanguage.UrlImage = _mediaService.GetPictureUrl(articleLanguage.ImageId.Value);
            }
            else
            {
                articleLanguage.UrlImage = "~/Content/images/no-image.png";
            }
            return articleLanguage;
        }

        public ArticleDetailModel GetArticleById(int id)
        {
            var currentLanguage = _workContext.CurrentLanguage;
            var defaultLanguage = _resourceService.DefaultLanguageId();

            var articleLanguage = _articleLanguageRepository.Table.Where(p => p.Id == id && p.Article.IsActive && p.LanguageId == currentLanguage).Select(p => new ArticleDetailModel
            {
                Id = p.ArticleId,
                HeadLine = p.HeadLine,
                Content = p.Content,
                ImageId = p.Article.FeatureImageId,
                Extract = p.Extract,
            }).FirstOrDefault();
            if (articleLanguage == null && currentLanguage == defaultLanguage)
            {
                return null;
            }

            articleLanguage = _articleLanguageRepository.Table.Where(p => p.Id == id && p.Article.IsActive && p.LanguageId == defaultLanguage).Select(p => new ArticleDetailModel
            {
                Id = p.ArticleId,
                HeadLine = p.HeadLine,
                Content = p.Content,
                ImageId = p.Article.FeatureImageId,
                Extract = p.Extract,
            }).FirstOrDefault();
            if (articleLanguage == null)
            {
                return null;
            }


            if (articleLanguage.ImageId.HasValue)
            {
                articleLanguage.UrlImage = _mediaService.GetPictureUrl(articleLanguage.ImageId.Value);
            }
            else
            {
                articleLanguage.UrlImage = "~/Content/images/no-image.png";
            }
            return articleLanguage;
        }

        public IPagedList<ArticleListingModel> GetNewArticles(GetArticlesRequest request)
        {
            if (request.PageIndex < 0) request.PageIndex = 0;
            if (request.PageSize < 1) request.PageSize = 10;
            var currentLanguage = _workContext.CurrentLanguage;

            var articleQuery = (from a in _articleRepository.Table.Where(p => p.IsActive == true && p.IsShowHomepage == true)
                                join al in _articleLanguageRepository.Table.Where(p => p.LanguageId == currentLanguage) on a.Id equals al.ArticleId
                                select new ArticleListingModel { Id = a.Id, ImageId = a.FeatureImageId, HeadLine = al.HeadLine, Extract = al.Extract, PushlishDate = a.PublishDate });






            var total = articleQuery.Count();
            var pagedArticles = articleQuery.OrderByDescending(p => p.PushlishDate).Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToList();

           
            foreach (var article in pagedArticles)
            {
                if (article.ImageId.HasValue)
                    article.UrlImage = _mediaService.GetPictureUrl(article.ImageId.Value);
                else
                    article.UrlImage = "~/Content/images/no-image.png";
            };


            return new PagedList<ArticleListingModel>(pagedArticles, request.PageIndex, request.PageSize, total);

        }

        public IList<ArticleListingModel> GetSession(bool flag) // request == true => get session1 
        {
            var currentLanguage = _workContext.CurrentLanguage;
            if (flag == true)
            {               
                var articleQuery = (from a in _articleRepository.Table.Where(p => p.Section1 == true && p.IsActive == true && p.IsShowHomepage == true)
                                    join al in _articleLanguageRepository.Table.Where(p => p.LanguageId == currentLanguage) on a.Id equals al.ArticleId
                                    select new ArticleListingModel { Id = a.Id, ImageId = a.FeatureImageId, HeadLine = al.HeadLine, Extract = al.Extract, PushlishDate = a.PublishDate });

                var total = articleQuery.Count();
                var Articles = articleQuery.OrderByDescending(p => p.PushlishDate).Take(5).ToList();


                foreach (var article in Articles)
                {
                    if (article.ImageId.HasValue)
                        article.UrlImage = _mediaService.GetPictureUrl(article.ImageId.Value);
                    else
                        article.UrlImage = "~/Content/images/no-image.png";
                };
                return Articles;

            }
            else
            {
                var articleQuery = (from a in _articleRepository.Table.Where(p => p.Section2 == true && p.IsActive == true && p.IsShowHomepage == true)
                                    join al in _articleLanguageRepository.Table.Where(p => p.LanguageId == currentLanguage) on a.Id equals al.ArticleId
                                    select new ArticleListingModel { Id = a.Id, ImageId = a.FeatureImageId, HeadLine = al.HeadLine, Extract = al.Extract, PushlishDate = a.PublishDate });

                var total = articleQuery.Count();
                var Articles = articleQuery.OrderByDescending(p => p.PushlishDate).Take(5).ToList();


                foreach (var article in Articles)
                {
                    if (article.ImageId.HasValue)
                        article.UrlImage = _mediaService.GetPictureUrl(article.ImageId.Value);
                    else
                        article.UrlImage = "~/Content/images/no-image.png";
                };
                return Articles;
            }
            
        }
    }
}
