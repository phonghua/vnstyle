using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Ricky.Infrastructure.Core;
using Ricky.Infrastructure.Core.ObjectContainer;
using VnStyle.Services.Data;
using VnStyle.Services.Data.Domain;

namespace VnStyle.Web.Controllers.Api
{
    [RoutePrefix("api/articles")]
    public class ArticlesController : BaseController
    {
        #region "Fields and Property"
        private readonly IBaseRepository<Article> _articleRepository;
        private readonly IBaseRepository<ArticleLanguage> _articleLanguageRepository;
        #endregion


        public ArticlesController()
        {
            _articleRepository = EngineContext.Current.Resolve<IBaseRepository<Article>>();
            _articleLanguageRepository = EngineContext.Current.Resolve<IBaseRepository<ArticleLanguage>>();
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
            var article = _articleRepository.Table.AsNoTracking().Where(p => p.Id == id).Include(p => p.ArticleLanguages)
                .Include("ArticleLanguages.MetaTag").Select(p => new
                {
                    Id = p.Id,
                    CreatedDate = p.CreatedDate,
                    HeadLine = p.HeadLine,
                    ModifiedDate = p.ModifiedDate,
                    PublishDate = p.PublishDate,
                    State = p.State,
                    ArticleLanguages = p.ArticleLanguages.Select(al => new { Id = al.Id, al.ArticleId, al.Content, al.Extract, al.HeadLine, al.LanguageId, al.MetaTag, al.MetaTagId })
                }).FirstOrDefault();
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
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
