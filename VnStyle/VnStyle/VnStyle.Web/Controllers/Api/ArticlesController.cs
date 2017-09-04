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
        private readonly IBaseRepository<ArticleLanguage> _articleLanguageRepository;
        private readonly IMediaService _mediaService;
        private readonly IWebHelper _webHelper;
        private readonly IResourceService _resourceService;
        #endregion


        public ArticlesController()
        {
            _articleRepository = EngineContext.Current.Resolve<IBaseRepository<Article>>();
            _articleLanguageRepository = EngineContext.Current.Resolve<IBaseRepository<ArticleLanguage>>();
            _mediaService = EngineContext.Current.Resolve<IMediaService>();
            _webHelper = EngineContext.Current.Resolve<IWebHelper>();
            _resourceService = EngineContext.Current.Resolve<IResourceService>();
        }


        [Route("")]
        public async Task<HttpResponseMessage> GetArticles()
        {
            var articles = (from a in _articleRepository.Table select a).AsNoTracking().ToList();
            return Request.CreateResponse(HttpStatusCode.OK, articles);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetArticle(int id)
        {
            var currentHosting = _webHelper.GetStoreHost(_webHelper.IsCurrentConnectionSecured()).TrimEnd('/');
            var article = _articleRepository.Table.AsNoTracking().Where(p => p.Id == id).Include(p => p.ArticleLanguages)
                .Include("ArticleLanguages.MetaTag").Select(p => new ArticleModelView
                {
                    Id = p.Id,
                    CreatedDate = p.CreatedDate,
                    HeadLine = p.HeadLine,
                    ModifiedDate = p.ModifiedDate,
                    PublishDate = p.PublishDate,
                    State = p.State,
                    FeatureImageId = p.FeatureImageId,
                    ArticleLanguages = p.ArticleLanguages.Select(al => new ArticleLanguageModelView { Id = al.Id, Content = al.Content, Extract = al.Extract, HeadLine = al.HeadLine, LanguageId = al.LanguageId, MetaTag = al.MetaTag, MetaTagId = al.MetaTagId })
                }).FirstOrDefault();
            if (article != null && article.FeatureImageId.HasValue)
            {
                article.FeatureImage = new ImageModelView { ImageUrl = $"{currentHosting}{ _mediaService.GetPictureUrl(article.FeatureImageId.Value)}", ImageId = article.FeatureImageId.Value };
            }
            return Request.CreateResponse(HttpStatusCode.OK, article);
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

            return CreateResponseMessage();
        }


        [Route("")]
        [HttpPut]
        public HttpResponseMessage Put(Article article)
        {

            //_articleRepository.Update(article);
            var entity = _articleRepository.Table.Where(p => p.Id == article.Id).Include(p=> p.ArticleLanguages)
                .Include("ArticleLanguages.MetaTag")
                .FirstOrDefault();
            if (entity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);



            entity.HeadLine = article.ArticleLanguages.Where(p => p.LanguageId == _resourceService.DefaultLanguageId())
                .Select(p => p.HeadLine).FirstOrDefault();

            //entity.ArticleLanguages = article.ArticleLanguages;

            foreach (var entityArticleLanguage in entity.ArticleLanguages)
            {
                var obj = article.ArticleLanguages.FirstOrDefault(p => p.Id == entityArticleLanguage.Id);
                if (obj != null)
                {
                    entityArticleLanguage.HeadLine = obj.HeadLine;
                    entityArticleLanguage.Content = obj.Content;
                    entityArticleLanguage.Extract = obj.Extract;
                    entityArticleLanguage.HeadLine = obj.HeadLine;
                    entityArticleLanguage.HeadLine = obj.HeadLine;
                    entityArticleLanguage.HeadLine = obj.HeadLine;
                    entityArticleLanguage.HeadLine = obj.HeadLine;
                    entityArticleLanguage.HeadLine = obj.HeadLine;
                }
            }

            _articleRepository.Update(entity);
            _articleRepository.SaveChanges();
            return CreateResponseMessage();
        }
    }
}
