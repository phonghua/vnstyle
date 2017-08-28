using System;
using System.Collections.Generic;
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
    [RoutePrefix("api/languages")]
    public class LanguagesController : BaseController
    {
        #region "Fields and Property"
        private readonly IBaseRepository<Language> _languagesRepository;
        private readonly IBaseRepository<Article> _articleRepository;
        #endregion


        public LanguagesController()
        {
            _languagesRepository = EngineContext.Current.Resolve<IBaseRepository<Language>>();
            _articleRepository = EngineContext.Current.Resolve<IBaseRepository<Article>>();
        }


        [Route("")]
        public async Task<HttpResponseMessage> Get()
        {
            var languages = await _languagesRepository.Table.ToListAsync();
            return Request.CreateResponse(HttpStatusCode.OK, languages);
        }

        
    }
}
