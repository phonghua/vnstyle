using Ricky.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VnStyle.Services.Business.Models;
using VnStyle.Services.Data;
using VnStyle.Services.Data.Domain;
using VnStyle.Services.Data.Enum;
using Ricky.Infrastructure.Core.Generic;

namespace VnStyle.Services.Business
{
    public class ArticleService : IArticleService
    {
        #region
        private readonly IBaseRepository<Article> _articleRepository;
        private readonly IBaseRepository<HomePageFeaturedArticle> _homePageFeaturedArticleRepository;
        private readonly IBaseRepository<ArticleLanguage> _articleLanguageRepository;
        private readonly IBaseRepository<RelatedArticle> _relatedArticleRepository;
        private readonly IBaseRepository<MetaTag> _metaTagRepository;
        private readonly IWorkContext _workContext;
        private readonly IResourceService _resourceService;
        private readonly IMediaService _mediaService;

        public ArticleService(IBaseRepository<Article> articleRepository, IBaseRepository<ArticleLanguage> articleLanguageRepositor, IWorkContext workContext, IResourceService resourceService, IMediaService mediaService, IBaseRepository<RelatedArticle> relatedArticleRepository, IBaseRepository<HomePageFeaturedArticle> homePageFeaturedArticleRepository, IBaseRepository<MetaTag> metaTagRepository)
        {
            _articleRepository = articleRepository;
            _homePageFeaturedArticleRepository = homePageFeaturedArticleRepository;
            _metaTagRepository = metaTagRepository;
            _articleLanguageRepository = articleLanguageRepositor;
            _workContext = workContext;
            _resourceService = resourceService;
            _mediaService = mediaService;
            _relatedArticleRepository = relatedArticleRepository;
        }
        #endregion

        public IPagedList<ArticleListingModel> GetArticles(GetArticlesRequest request)
        {
            if (request.PageIndex < 0) request.PageIndex = 0;
            if (request.PageSize < 1) request.PageSize = 10;
            var currentLanguage = _workContext.CurrentLanguage;

            var articleQuery = (from a in _articleRepository.Table.Where(p => p.RootCate == request.RootCate && p.IsActive == true)
                                join al in _articleLanguageRepository.Table.Where(p => p.LanguageId == currentLanguage) on a.Id equals al.ArticleId

                                select new ArticleListingModel
                                {
                                    Id = a.Id,
                                    ImageId = a.FeatureImageId,
                                    HeadLine = al.HeadLine,
                                    Extract = al.Extract,
                                    PushlishDate = a.PublishDate
                                });
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
            //var currentLanguage = "en";
            var defaultLanguage = _resourceService.DefaultLanguageId();

            
            var articleLanguage = _articleLanguageRepository.Table.Where(p => p.ArticleId == id && p.Article.IsActive && p.LanguageId == currentLanguage).Select(p => new ArticleDetailModel
            {
                Id = p.ArticleId,
                HeadLine = p.HeadLine,
                Content = p.Content,
                ImageId = p.Article.FeatureImageId,
                Extract = p.Extract,
                MetaTagId = p.MetaTagId,
                ListRelatedArticles = _relatedArticleRepository.Table.Where(a => a.Article1Id == p.ArticleId).OrderBy(a => a.Seq).Select(a => new RelatedArticlesMap { Id = a.Article2Id, HeadLine = a.Article2.HeadLine, ImageId = a.Article2.FeatureImageId }).ToList()
            }).FirstOrDefault();
            if (articleLanguage == null && currentLanguage == defaultLanguage)
            {
                return null;
            }

            
            if (articleLanguage == null)
            {
                articleLanguage = _articleLanguageRepository.Table.Where(p => p.ArticleId == id && p.Article.IsActive && p.LanguageId == defaultLanguage).Select(p => new ArticleDetailModel
                {
                    Id = p.ArticleId,
                    HeadLine = p.HeadLine,
                    Content = p.Content,
                    ImageId = p.Article.FeatureImageId,
                    Extract = p.Extract,
                    MetaTagId = p.MetaTagId,
                    ListRelatedArticles = _relatedArticleRepository.Table.Where(a => a.Article1Id == p.ArticleId).OrderBy(a => a.Seq).Select(a => new RelatedArticlesMap { Id = a.Article2Id, HeadLine = a.Article2.HeadLine, ImageId = a.Article2.FeatureImageId }).ToList()
                }).FirstOrDefault();
            }


            if (articleLanguage.ImageId.HasValue)
            {
                articleLanguage.UrlImage = _mediaService.GetPictureUrl(articleLanguage.ImageId.Value);
            }
            else
            {
                articleLanguage.UrlImage = "~/Content/images/no-image.png";
            }
            foreach (var article in articleLanguage.ListRelatedArticles)
            {
                if (article.ImageId.HasValue)
                {
                    article.UrlImage = _mediaService.GetPictureUrl(article.ImageId.Value);
                }
                else
                {
                    articleLanguage.UrlImage = "~/Content/images/no-image.png";
                }
            }
            return articleLanguage;
        }

        public IPagedList<ArticleListingModel> GetNewArticles(GetArticlesRequest request)
        {
            if (request.PageIndex < 0) request.PageIndex = 0;
            if (request.PageSize < 1) request.PageSize = 10;
            var currentLanguage = _workContext.CurrentLanguage;
            var defaultLanguage = _resourceService.DefaultLanguageId();
            var articleQuery = (from a in _articleRepository.Table.Where(p => p.IsActive == true && p.IsShowHomepage == true)
                                join al in _articleLanguageRepository.Table.Where(p => p.LanguageId == currentLanguage) on a.Id equals al.ArticleId
                                select new ArticleListingModel
                                {
                                    Id = a.Id,
                                    ImageId = a.FeatureImageId,
                                    HeadLine = al.HeadLine,
                                    Extract = al.Extract,
                                    PushlishDate = a.PublishDate,
                                    CateId = a.RootCate
                                });
            if (!articleQuery.Any())
            {
                articleQuery = (from a in _articleRepository.Table.Where(p => p.IsActive == true && p.IsShowHomepage == true)
                                join al in _articleLanguageRepository.Table.Where(p => p.LanguageId == defaultLanguage) on a.Id equals al.ArticleId
                                select new ArticleListingModel
                                {
                                    Id = a.Id,
                                    ImageId = a.FeatureImageId,
                                    HeadLine = al.HeadLine,
                                    Extract = al.Extract,
                                    PushlishDate = a.PublishDate,
                                    CateId = a.RootCate
                                });
            }
            var total = articleQuery.Count();
            var pagedArticles = articleQuery.OrderByDescending(p => p.PushlishDate).Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToList();


            foreach (var article in pagedArticles)
            {
                if (article.ImageId.HasValue)
                    article.UrlImage = _mediaService.GetPictureUrl(article.ImageId.Value);
                else
                    article.UrlImage = "~/Content/images/no-image.png";
            };
            foreach(var article in pagedArticles)
            {
                if( article.CateId == 3)
                    article.CateName = "RootCate_Event";
                if (article.CateId == 5)
                    article.CateName = "RootCate_Course";
                if (article.CateId == 6)
                    article.CateName = "RootCate_Tattoo";
                if (article.CateId == 7)
                    article.CateName = "RootCate_Piercing";        
            }
            return new PagedList<ArticleListingModel>(pagedArticles, request.PageIndex, request.PageSize, total);
        }

        public IList<ArticleListingModel> GetSession(int section) // request == true => get session1 
        {
            var currentLanguage = _workContext.CurrentLanguage;

            var defaultLanguage = _resourceService.DefaultLanguageId();
            if (section == 1)
            {

                var query = (from al in _articleLanguageRepository.Table.Where(p => p.LanguageId == currentLanguage)
                             join a in _articleRepository.Table.Where(p => p.Section1 == true && p.IsActive == true && p.IsShowHomepage == true) on al.ArticleId equals a.Id
                             select new ArticleListingModel { Id = a.Id, ImageId = a.FeatureImageId, HeadLine = al.HeadLine, Extract = al.Extract, PushlishDate = a.PublishDate });
                if (query == null && currentLanguage == defaultLanguage)
                {
                    return null;
                }


                if ( !query.Any())
                {
                    query = (from al in _articleLanguageRepository.Table.Where(p => p.LanguageId == defaultLanguage)
                             join a in _articleRepository.Table.Where(p => p.Section1 == true && p.IsActive == true && p.IsShowHomepage == true) on al.ArticleId equals a.Id
                             select new ArticleListingModel { Id = a.Id, ImageId = a.FeatureImageId, HeadLine = al.HeadLine, Extract = al.Extract, PushlishDate = a.PublishDate });
                }
                var total = query.Count();
                var Articles = query.OrderByDescending(p => p.PushlishDate).Take(4).ToList();


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
                var query = (from al in _articleLanguageRepository.Table.Where(p => p.LanguageId == currentLanguage)
                             join a in _articleRepository.Table.Where(p => p.Section2 == true && p.IsActive == true && p.IsShowHomepage == true) on al.ArticleId equals a.Id
                             select new ArticleListingModel { Id = a.Id, ImageId = a.FeatureImageId, HeadLine = al.HeadLine, Extract = al.Extract, PushlishDate = a.PublishDate });
                if (query == null && currentLanguage == defaultLanguage)
                {
                    return null;
                }


                if (!query.Any())
                {
                    query = (from al in _articleLanguageRepository.Table.Where(p => p.LanguageId == defaultLanguage)
                             join a in _articleRepository.Table.Where(p => p.Section2 == true && p.IsActive == true && p.IsShowHomepage == true) on al.ArticleId equals a.Id
                             select new ArticleListingModel { Id = a.Id, ImageId = a.FeatureImageId, HeadLine = al.HeadLine, Extract = al.Extract, PushlishDate = a.PublishDate });
                }
                var total = query.Count();
                var Articles = query.OrderByDescending(p => p.PushlishDate).Take(4).ToList();


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

        public FeaturedDetailModel GetFirstHomePageFeaturedArticles()
        {


            //var query = _homePageFeaturedArticleRepository.Table.OrderBy(p => p.Seq).Take(1);
            //var queryJoin1 = from q in query join al in _articleLanguageRepository.Table.Where(p => p.LanguageId == currentLanguage) on q.ArticleId equals al.ArticleId select new FeaturedDetailModel { Id = q.ArticleId , Seq = q.Seq , HeadLine = al.HeadLine};

            //return queryJoin1.First();


            //throw new NotImplementedException();
            var defaultLanguage = _resourceService.DefaultLanguageId();
            var currentLanguage = _workContext.CurrentLanguage;
            var query = _homePageFeaturedArticleRepository.Table.OrderBy(p => p.Seq).Take(1);
            var featuredLang = (from q in query                               
                                join a in _articleRepository.Table on q.ArticleId equals a.Id
                                join al in _articleLanguageRepository.Table.Where(p => p.LanguageId == currentLanguage) on a.Id equals al.ArticleId
                                select new FeaturedDetailModel { Id = q.ArticleId, HeadLine = al.HeadLine, ImageId = a.FeatureImageId, PushlishDate = a.PublishDate }).FirstOrDefault();

            if (featuredLang == null)
            {
                featuredLang = (from q in query
                                orderby q.Seq
                                join a in _articleRepository.Table on q.ArticleId equals a.Id
                                join al in _articleLanguageRepository.Table.Where(p => p.LanguageId == defaultLanguage) on a.Id equals al.ArticleId
                                select new FeaturedDetailModel { Id = q.ArticleId, HeadLine = al.HeadLine, ImageId = a.FeatureImageId, PushlishDate = a.PublishDate }).FirstOrDefault();
            }

            if (featuredLang == null) return null;

            if (featuredLang.ImageId.HasValue)
                featuredLang.UrlImage = _mediaService.GetPictureUrl(featuredLang.ImageId.Value);
            if (featuredLang.ImageId.HasValue)
                featuredLang.UrlImage = _mediaService.GetPictureUrl(featuredLang.ImageId.Value);
            else
                featuredLang.UrlImage = "~/Content/images/no-image.png";
            return featuredLang;



        }
        public IEnumerable<ArticleListingModel> GetLastHomePageFeaturedArticles()
        {
            //throw new NotImplementedException();
            var currentLanguage = _workContext.CurrentLanguage;

            var featuredLang = (from hp in _homePageFeaturedArticleRepository.Table
                            join a in _articleRepository.Table on hp.ArticleId equals a.Id
                            join al in _articleLanguageRepository.Table.Where(p => p.LanguageId == currentLanguage) on a.Id equals al.ArticleId
                            select new ArticleListingModel { Id = hp.ArticleId, HeadLine = al.HeadLine, ImageId = a.FeatureImageId, PushlishDate = a.PublishDate }).OrderBy(p => p.Id).Skip(1).Take(2).ToList();
            if (!featuredLang.Any())
            {
                featuredLang = (from hp in _homePageFeaturedArticleRepository.Table
                                join a in _articleRepository.Table on hp.ArticleId equals a.Id                                
                                select new ArticleListingModel { Id = hp.ArticleId, HeadLine = a.HeadLine, ImageId = a.FeatureImageId, PushlishDate = a.PublishDate }).OrderBy(p => p.Id).Skip(1).Take(2).ToList();
            }
            foreach (var article in featuredLang)
            {
                if (article.ImageId.HasValue)
                    article.UrlImage = _mediaService.GetPictureUrl(article.ImageId.Value);
                else
                    article.UrlImage = "~/Content/images/no-image.png";
            }
            return featuredLang;
        }

        public IPagedList<ArticleListingModel> GetArticlesByString(string search, PagingRequest request)
        {
            //throw new NotImplementedException();
            if (request.PageIndex < 0) request.PageIndex = 0;
            if (request.PageSize < 1) request.PageSize = 10;
            var currentLanguage = _workContext.CurrentLanguage;

            var query = _articleRepository.Table.Where(p => p.IsActive);
            search = search.ToLower().Trim();

            var query1 = query.Where(p =>  p.HeadLine.ToLower().Contains(search));
            var articleQuery = (from a in query1
                                join al in _articleLanguageRepository.Table.Where(p => p.LanguageId == currentLanguage) on a.Id equals al.ArticleId
                                select new ArticleListingModel
                                {
                                    Id = a.Id,
                                    ImageId = a.FeatureImageId,
                                    HeadLine = al.HeadLine,
                                    Extract = al.Extract,
                                    PushlishDate = a.PublishDate
                                });
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

        public MetaTag GetMetaTagById(int metaTagId)
        {
            return _metaTagRepository.Table.FirstOrDefault(p => p.Id == metaTagId);
        }

        //public FeaturedDetailModel GetFeaturedFirst()
        //{
        //    var query = _homePageFeaturedArticleRepository.Table.OrderBy(p => p.Seq);
             
        //    //.Select(p => new FeaturedDetailModel { Id = p.ArticleId, Seq = p.Seq }).FirstOrDefault();
        //    return query;
        //}

        //public ArticleListingModel GetFirstArticles()
        //{
        //    var article = _homePageFeaturedArticleRepository.Table.Select(p => new ArticleListingModel { Id = p.Id }).FirstOrDefault();
        //    return article;
        //}
    }
}
