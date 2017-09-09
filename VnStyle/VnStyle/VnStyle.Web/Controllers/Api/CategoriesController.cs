using Ricky.Infrastructure.Core.ObjectContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VnStyle.Services.Business;
using VnStyle.Services.Data;
using VnStyle.Services.Data.Domain;
using VnStyle.Services.Data.Enum;

namespace VnStyle.Web.Controllers.Api
{
    [RoutePrefix("api/categories")]
    public class CategoriesController : BaseController
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IRootCategoryService _rootCategoryService;

        public CategoriesController()
        {
            _rootCategoryService = EngineContext.Current.Resolve<IRootCategoryService>();
            _categoryRepository = EngineContext.Current.Resolve<IBaseRepository<Category>>();
        }

        [Route("{rootCateId}/query")]
        public async Task<HttpResponseMessage> Get(int rootCateId)
        {
            var query = _categoryRepository.Table.Where(p => (int)p.RootCategory == rootCateId);
            return Request.CreateResponse(HttpStatusCode.OK, query.ToList());
        }

        [Route("root-categories/article")]
        public HttpResponseMessage GetArticleCategories()
        {
            var articleCate = new List<ERootCategory> { ERootCategory.Intro, ERootCategory.Event, ERootCategory.Course, ERootCategory.Tattoo, ERootCategory.Piercing };
            return Request.CreateResponse(HttpStatusCode.OK, this._rootCategoryService.GetAllRootCategories().Where(p => articleCate.Contains((ERootCategory)p.Id)));
        }

        [Route("root-categories/gallery-photo")]
        public HttpResponseMessage GetGalleryPhotoCategories()
        {
            var articleCate = new List<ERootCategory> { ERootCategory.Image };
            return Request.CreateResponse(HttpStatusCode.OK, this._rootCategoryService.GetAllRootCategories().Where(p => articleCate.Contains((ERootCategory)p.Id)));
        }
    }
}
