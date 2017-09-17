using System;
using System.Collections.Generic;
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

namespace VnStyle.Web.Controllers.Api
{
    [RoutePrefix("api/galleries")]
    public class GalleryController : BaseController
    {
        #region "Fields and Properties"
        private readonly IBaseRepository<Article> _articleRepository;
        private readonly IBaseRepository<GalleryPhoto> _galleryPhotoRepository;
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IBaseRepository<ArticleLanguage> _articleLanguageRepository;
        private readonly IBaseRepository<RelatedArticle> _relatedArticleRepository;
        private readonly IMediaService _mediaService;
        private readonly IWebHelper _webHelper;
        private readonly IResourceService _resourceService;
        #endregion

        public GalleryController()
        {
            _articleRepository = EngineContext.Current.Resolve<IBaseRepository<Article>>();
            _categoryRepository = EngineContext.Current.Resolve<IBaseRepository<Category>>();
            _articleLanguageRepository = EngineContext.Current.Resolve<IBaseRepository<ArticleLanguage>>();
            _mediaService = EngineContext.Current.Resolve<IMediaService>();
            _webHelper = EngineContext.Current.Resolve<IWebHelper>();
            _resourceService = EngineContext.Current.Resolve<IResourceService>();
            _relatedArticleRepository = EngineContext.Current.Resolve<IBaseRepository<RelatedArticle>>();
            _galleryPhotoRepository = EngineContext.Current.Resolve<IBaseRepository<GalleryPhoto>>();
        }


        [Route("photo")]
        public async Task<HttpResponseMessage> GetPhotos(int? artistId = null)
        {
            var currentHosting = _webHelper.GetStoreHost(_webHelper.IsCurrentConnectionSecured()).TrimEnd('/');
            var photos = await _galleryPhotoRepository.Table.AsNoTracking().Where(p => p.ArtistId == artistId).ToListAsync();
            var result = photos.Select(p =>
            {
                return new { p.Id, Url = currentHosting + _mediaService.GetPictureUrl(p.FileId) };
            });
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

    }
}
