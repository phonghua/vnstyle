using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

        private readonly VnStyle.Services.Business.IResourceService _resourceService;
        #endregion


        public LanguagesController()
        {
            _resourceService = EngineContext.Current.Resolve<Services.Business.IResourceService>();
        }


        [Route("")]
        public async Task<HttpResponseMessage> Get()
        {
            var languages = _resourceService.GetLanguages();
            return Request.CreateResponse(HttpStatusCode.OK, languages);
        }

        
    }
}
