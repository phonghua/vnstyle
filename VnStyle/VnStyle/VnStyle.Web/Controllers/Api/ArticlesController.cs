using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
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
        #endregion


        public ArticlesController()
        {
            _articleRepository = EngineContext.Current.Resolve<IBaseRepository<Article>>();
        }

        
        [Route("")]
        public async Task<HttpResponseMessage> GetArticles()
        {
            var articles = await _articleRepository.Table.ToListAsync();
            return Request.CreateResponse(HttpStatusCode.OK, articles);
        }
    }
}
