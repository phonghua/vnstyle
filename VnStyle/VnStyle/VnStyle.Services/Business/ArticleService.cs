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
        
        public IPagedList<ArticleModelView> GetArticles(ArticleModelRequest request)
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
            var total = query.Count();
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
            return new PagedList<ArticleModelView>(query, request.PageIndex, request.PageSize, total);


        }

        public ArticleModelView GetArticleIntro(ArticleModelRequest request)
        {
            var articleLanguage = _articleLanguageRepository.Table.Where(p => p.Article.RootCate == request.rootCate).FirstOrDefault(p => p.LanguageId == request.currentLanguage);
            if (articleLanguage == null && request.currentLanguage == request.defaultLanguage)
            {
                return null;
            }
            articleLanguage = _articleLanguageRepository.Table.Where(p => p.Article.RootCate == (int)ERootCategory.Intro && p.Article.IsActive == true).FirstOrDefault(p => p.LanguageId == request.defaultLanguage);

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
            var articleLanguage = _articleLanguageRepository.Table.Where(p => p.ArticleId == id).Select(p => new
            {
                Id = p.ArticleId,
                HeadLine = p.HeadLine,
                Content = p.Content,
                ImageId = p.Article.FeatureImageId,
                CreatedDate = p.Article.CreatedDate,
                Extract = p.Extract,
                LanguageId = p.LanguageId
            }).FirstOrDefault(p => p.LanguageId == request.currentLanguage);
            if (articleLanguage == null && request.currentLanguage == request.defaultLanguage)
            {
                return null;
            }
            articleLanguage = _articleLanguageRepository.Table.Where(p => p.ArticleId == id).Select(p => new
            {
                Id = p.ArticleId,
                HeadLine = p.HeadLine,
                Content = p.Content,
                ImageId = p.Article.FeatureImageId,
                CreatedDate = p.Article.CreatedDate,
                Extract = p.Extract,
                LanguageId = p.LanguageId
            }).FirstOrDefault(p => p.LanguageId == request.defaultLanguage);
            if (articleLanguage == null && request.currentLanguage == request.defaultLanguage)
            {
                return null;
            }

            var model = new ArticleModelView
            {
                Id = articleLanguage.Id,
                Headline = articleLanguage.HeadLine,
                Content = articleLanguage.Content,
                ImageId = articleLanguage.ImageId,
                Extract = articleLanguage.Extract,
                CreatedDate = articleLanguage.CreatedDate
                
            };
            if (model.ImageId.HasValue)
            {
                model.UrlImage = _mediaService.GetPictureUrl(model.ImageId.Value);
            }
            else
            {
                model.UrlImage = "/Content/images/no-image.png";
            }
            return model;

        }

        public IPagedList<ArticleModelView> GetArticlesNew(ArticleModelRequest request)
        {
            var articleLanguage = _articleLanguageRepository.Table.Where(p => p.Article.IsShowHomepage && p.Article.IsActive == true && p.LanguageId == request.currentLanguage).Select(p => new
            {
                Id = p.ArticleId,
                HeadLine = p.HeadLine,
                Content = p.Content,
                ImageId = p.Article.FeatureImageId,
                CreatedDate = p.Article.CreatedDate,
                Extract = p.Extract,
                LanguageId = p.LanguageId
            }).ToList();
            if (articleLanguage == null && request.currentLanguage == request.defaultLanguage)
            {
                return null;
            }
            articleLanguage = _articleLanguageRepository.Table.Where(p => p.Article.IsShowHomepage && p.Article.IsActive == true && p.LanguageId == request.defaultLanguage).Select(p => new
            {
                Id = p.ArticleId,
                HeadLine = p.HeadLine,
                Content = p.Content,
                ImageId = p.Article.FeatureImageId,
                CreatedDate = p.Article.CreatedDate,
                Extract = p.Extract,
                LanguageId = p.LanguageId
            }).ToList();
            
            if (articleLanguage == null && request.currentLanguage == request.defaultLanguage)
            {
                return null;
            }
            var total = articleLanguage.Count();
            var model = articleLanguage.Select(p => new ArticleModelView
            {
                Id = p.Id,
                Headline = p.HeadLine,
                Content = p.Content,
                ImageId = p.ImageId,
                Extract = p.Extract,
                CreatedDate = p.CreatedDate
            });
            
            foreach (var article in model)
            {
                if (article.ImageId.HasValue)
                {
                    article.UrlImage = _mediaService.GetPictureUrl(article.ImageId.Value);
                }
                else
                {
                    article.UrlImage = "/Content/images/no-image.png";
                }
            }
            return new PagedList<ArticleModelView>(model, request.PageIndex, request.PageSize, total);
           
        }
    }
}
