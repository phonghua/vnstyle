using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Ricky.Infrastructure.Core;
using Ricky.Infrastructure.Core.ObjectContainer;
using VnStyle.Services.Business;
using VnStyle.Services.Data;
using VnStyle.Services.Data.Domain;
using VnStyle.Web.Controllers.Api.Models;

namespace VnStyle.Web.Controllers.Api
{
    [RoutePrefix("api/articles")]
    public class ArticlesController : BaseController
    {
        #region "Fields and Properties"
        private readonly IBaseRepository<Article> _articleRepository;
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IBaseRepository<ArticleLanguage> _articleLanguageRepository;
        private readonly IBaseRepository<RelatedArticle> _relatedArticleRepository;
        private readonly IMediaService _mediaService;
        private readonly IWebHelper _webHelper;
        private readonly IResourceService _resourceService;
        #endregion


        public ArticlesController()
        {
            _articleRepository = EngineContext.Current.Resolve<IBaseRepository<Article>>();
            _categoryRepository = EngineContext.Current.Resolve<IBaseRepository<Category>>();
            _articleLanguageRepository = EngineContext.Current.Resolve<IBaseRepository<ArticleLanguage>>();
            _mediaService = EngineContext.Current.Resolve<IMediaService>();
            _webHelper = EngineContext.Current.Resolve<IWebHelper>();
            _resourceService = EngineContext.Current.Resolve<IResourceService>();
            _relatedArticleRepository = EngineContext.Current.Resolve<IBaseRepository<RelatedArticle>>();
        }


        [Route("")]
        public async Task<HttpResponseMessage> GetArticles(int? rootCateId = null)
        {
            var query = from a in _articleRepository.Table select a;
            if (rootCateId.HasValue && rootCateId.Value > 0)
            {
                var cateIds = _categoryRepository.Table.Where(p => (int)p.RootCategory == rootCateId).Select(p => p.Id).ToList();
                //if (cateIds.Any())
                    query = query.Where(p => (cateIds.Any() && p.CategoryId.HasValue && cateIds.Contains(p.CategoryId.Value) || p.RootCate == rootCateId ));
                //else query = query.Take(0);
            }

            var articles = await query.AsNoTracking().ToListAsync();
            return Request.CreateResponse(HttpStatusCode.OK, articles);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetArticle(int id)
        {
            var currentHosting = _webHelper.GetStoreHost(_webHelper.IsCurrentConnectionSecured()).TrimEnd('/');
            var article = await _articleRepository.Table.AsNoTracking().Where(p => p.Id == id)
                .Include(p => p.ArticleLanguages)
                .Include("ArticleLanguages.MetaTag").Select(p => new ArticleModelView
                {
                    Id = p.Id,
                    CreatedDate = p.CreatedDate,
                    HeadLine = p.HeadLine,
                    ModifiedDate = p.ModifiedDate,
                    PublishDate = p.PublishDate,
                    State = p.State,
                    FeatureImageId = p.FeatureImageId,
                    ArticleLanguages = p.ArticleLanguages.Select(al => new ArticleLanguageModelView
                    {
                        Id = al.Id,
                        Content = al.Content,
                        Extract = al.Extract,
                        HeadLine = al.HeadLine,
                        LanguageId = al.LanguageId,
                        MetaTag = al.MetaTag,
                        MetaTagId = al.MetaTagId
                    })
                }).FirstOrDefaultAsync();
            if (article != null && article.FeatureImageId.HasValue)
            {
                article.FeatureImage = new ImageModelView { ImageUrl = $"{currentHosting}{ _mediaService.GetPictureUrl(article.FeatureImageId.Value)}", ImageId = article.FeatureImageId.Value };
            }
            return Request.CreateResponse(HttpStatusCode.OK, article);
        }

        [Route("{id}/related")]
        public async Task<HttpResponseMessage> GetRelatedArticles(int id)
        {
            var relatedArticles = await _relatedArticleRepository.Table.Where(p => p.Article1Id == id).Include(p => p.Article2).AsNoTracking().OrderBy(p => p.Seq).ThenBy(p => p.ModifiedDate)
                .Select(p => new { p.Article2.Id, p.Article2.HeadLine }).ToListAsync();

            return Request.CreateResponse(HttpStatusCode.OK, relatedArticles);
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage Post(Article article)
        {
            article.CreatedDate = DateTimeHelper.GetCurrentDateTime();
            article.ModifiedDate = DateTimeHelper.GetCurrentDateTime();
            article.PublishDate = DateTimeHelper.GetCurrentDateTime();

            article.HeadLine = article.ArticleLanguages.Select(p => p.HeadLine).FirstOrDefault();

            _articleRepository.Insert(article);
            _articleRepository.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [Route("")]
        [HttpPut]
        public HttpResponseMessage Put(Article article)
        {

            //_articleRepository.Update(article);
            var entity = _articleRepository.Table.Where(p => p.Id == article.Id).Include(p => p.ArticleLanguages)
                .Include("ArticleLanguages.MetaTag")
                .FirstOrDefault();
            if (entity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            entity.HeadLine = article.ArticleLanguages.Where(p => p.LanguageId == _resourceService.DefaultLanguageId()).Select(p => p.HeadLine).FirstOrDefault();
            entity.ModifiedDate = DateTimeHelper.GetCurrentDateTime();
            if (article.FeatureImageId.HasValue && entity.FeatureImageId != article.FeatureImageId) entity.FeatureImageId = article.FeatureImageId;

            foreach (var entityArticleLanguage in entity.ArticleLanguages)
            {
                var obj = article.ArticleLanguages.FirstOrDefault(p => p.Id == entityArticleLanguage.Id);
                if (obj != null)
                {
                    entityArticleLanguage.HeadLine = obj.HeadLine;
                    entityArticleLanguage.Content = obj.Content;
                    entityArticleLanguage.Extract = obj.Extract;
                    entityArticleLanguage.MetaTag.Description = obj.MetaTag.Description;
                    entityArticleLanguage.MetaTag.Keywords = obj.MetaTag.Keywords;
                }
            }

            _articleRepository.Update(entity);
            _articleRepository.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("{id}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            _articleLanguageRepository.DeleteRange(p => p.ArticleId == id);
            _relatedArticleRepository.DeleteRange(p => p.Article2Id == id || p.Article1Id == id);

            _articleRepository.DeleteRange(p => p.Id == id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("search")]
        [HttpGet]
        public async Task<HttpResponseMessage> Search(string query)
        {
            var articles = await this._articleRepository.Table.AsNoTracking().Select(p => new { p.Id, p.HeadLine }).ToListAsync();
            return Request.CreateResponse(HttpStatusCode.OK, articles);
        }

        [Route("{id}/related/{relatedArticleId}")]
        [HttpPut]
        public HttpResponseMessage PutRelatedArticle(int id, int relatedArticleId)
        {
            var nextSeq = _relatedArticleRepository.Any(p => p.Article1Id == id) ? _relatedArticleRepository.Table.Where(p => p.Article1Id == id).Max(p => p.Seq) + 1 : 1;
            _relatedArticleRepository.Insert(new RelatedArticle { Article1Id = id, Article2Id = relatedArticleId, Seq = nextSeq, ModifiedDate = DateTimeHelper.GetCurrentDateTime() });
            _relatedArticleRepository.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("{id}/related/{relatedArticleId}")]
        [HttpDelete]
        public HttpResponseMessage DeleteRelatedArticle(int id, int relatedArticleId)
        {
            _relatedArticleRepository.DeleteRange(p => p.Article1Id == id && p.Article2Id == relatedArticleId);
            _relatedArticleRepository.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("{id}/related/{relatedArticleId1}/swap/{relatedArticleId2}")]
        [HttpPut]
        public async Task<HttpResponseMessage> PutRelatedArticleSwap(int id, int relatedArticleId1, int relatedArticleId2)
        {
            var seq1 = await _relatedArticleRepository.Table.Where(p => p.Article1Id == id && p.Article2Id == relatedArticleId1).Select(p => p.Seq).FirstOrDefaultAsync();
            var seq2 = await _relatedArticleRepository.Table.Where(p => p.Article1Id == id && p.Article2Id == relatedArticleId2).Select(p => p.Seq).FirstOrDefaultAsync();

            _relatedArticleRepository.Update(p => p.Article1Id == id && p.Article2Id == relatedArticleId2, p => new RelatedArticle() { Seq = seq1 });
            _relatedArticleRepository.Update(p => p.Article1Id == id && p.Article2Id == relatedArticleId1, p => new RelatedArticle() { Seq = seq2 });



            _relatedArticleRepository.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
