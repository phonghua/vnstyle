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
        public IEnumerable<ArticleModelView> GetArticles(ArticleModelRequest request)
        {



            var articles = (from a in _articleRepository.Table.Where(p => p.RootCate == request.rootCate && p.IsActive == true)
                            join al in _articleLanguageRepository.Table.Where(p => p.LanguageId == request.currentLanguage) on a.Id equals al.ArticleId
                            select new { a.Id, a.FeatureImageId, al.Content, al.HeadLine, al.Extract, a.CreatedDate }).ToList();

            if (articles == null && request.currentLanguage == request.defaultLanguage)
            {
                //... return NOT FOUND
            }


            var query = articles.Select(p => new ArticleModelView
            {
                Id = p.Id,
                Content = p.Content,
                Headline = p.HeadLine,
                //UrlImage = _mediaService.GetPictureUrl((long)p.FeatureImageId),
                ImageId = p.FeatureImageId,
                Extract = p.Extract,
                CreatedDate = p.CreatedDate


            }).ToList();
            foreach (var a in query)
            {
                if (a.ImageId.HasValue)
                {
                    a.UrlImage = _mediaService.GetPictureUrl(a.ImageId.Value);
                }
                else
                {
                    a.UrlImage = "/Content/images/no-image.png";
                }
            }
            return query;


        }

        public ArticleModelView GetArticleIntro(ArticleModelRequest request)
        {
            var articleLanguage = _articleLanguageRepository.Table.Where(p => p.Article.RootCate == request.rootCate).FirstOrDefault(p => p.LanguageId == request.currentLanguage);
            if (articleLanguage == null && request.currentLanguage == request.defaultLanguage)
            {
                return null;
            }
            articleLanguage = _articleLanguageRepository.Table.Where(p => p.Article.RootCate == (int)ERootCategory.Intro).FirstOrDefault(p => p.LanguageId == request.defaultLanguage);

            if (articleLanguage == null)
            {
                return null;
            }

            var model = new ArticleModelView
            {
                Headline = articleLanguage.HeadLine,
                Content = articleLanguage.Content

            };
            return model;
        }

        public ArticleModelView GetArticleById(int id, ArticleModelRequest request)
        {
            var article = _articleRepository.Table.Where(p => p.Id == id).Select(a => new ArticleModelView
            {
                Id = a.Id,
                Headline = a.HeadLine,
                ImageId = a.FeatureImageId
            }).FirstOrDefault();
            if (article == null) return null;
            if (!article.ImageId.HasValue)
            {
                article.UrlImage = "/Content/images/no-image.png";
            }
            else
            {
                article.UrlImage = _mediaService.GetPictureUrl(article.ImageId.Value);
            }
            return article;

        }
    }
}

// 
