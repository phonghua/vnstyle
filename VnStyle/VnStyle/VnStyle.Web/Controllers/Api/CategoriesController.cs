using Ricky.Infrastructure.Core.ObjectContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VnStyle.Services.Data;
using VnStyle.Services.Data.Domain;

namespace VnStyle.Web.Controllers.Api
{
    [RoutePrefix("api/categories")]
    public class CategoriesController : BaseController
    {
        private readonly IBaseRepository<Category> _categoryRepository;

        public CategoriesController()
        {
            _categoryRepository = EngineContext.Current.Resolve<IBaseRepository<Category>>();
        }

        [Route("{rootCateId}/query")]
        public async Task<HttpResponseMessage> Get(int rootCateId)
        {
            var query = _categoryRepository.Table.Where(p => (int)p.RootCategory == rootCateId);
            return Request.CreateResponse(HttpStatusCode.OK,query.ToList());        
        }
    }
}
