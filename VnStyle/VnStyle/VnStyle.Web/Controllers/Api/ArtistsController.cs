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
    [RoutePrefix("api/artists")]
    public class ArtistsController : BaseController
    {
        #region "Fields and Properties"
        private readonly IBaseRepository<Artist> _artistRepository;
        private readonly IResourceService _resourceService;
        private readonly IMediaService _mediaService;
        private readonly IWebHelper _webHelper;
        private readonly IBaseRepository<GalleryPhoto> _galleryPhotoRepository;

        public ArtistsController()
        {
            _artistRepository = EngineContext.Current.Resolve<IBaseRepository<Artist>>();
            _resourceService = EngineContext.Current.Resolve<IResourceService>();
            _mediaService = EngineContext.Current.Resolve<IMediaService>();
            _webHelper = EngineContext.Current.Resolve<IWebHelper>();
            _galleryPhotoRepository = EngineContext.Current.Resolve<IBaseRepository<GalleryPhoto>>();
        }

        #endregion

        [Route("")]
        public async Task<IHttpActionResult> GetArtists()
        {
            var currentHosting = _webHelper.GetStoreHost(_webHelper.IsCurrentConnectionSecured()).TrimEnd('/');
            var artists = await _artistRepository.Table.OrderBy(p => p.Seq).ToListAsync();
            var imageUrls = artists.Where(p => p.ImageId > 0).Select(p => p.ImageId)
                .Select(p => new { ImageId = p, ImageUrl = $"{currentHosting}{_mediaService.GetPictureUrl(p)}" });

            return Ok(artists.Select(p =>
            {
                var imageUrl = imageUrls.Where(i => i.ImageId == p.ImageId).Select(i => i.ImageUrl).FirstOrDefault();
                return new
                {
                    p.Id,
                    p.ImageId,
                    p.Name,
                    p.Seq,
                    p.ShowOnHompage,
                    ImageUrl = imageUrl,
                    Image = new
                    {
                        imageId = p.ImageId,
                        imageUrl = imageUrl
                    }

                };
            }));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult PostArtist(Artist artist)
        {
            _artistRepository.Insert(artist);
            _artistRepository.SaveChanges();
            return Ok(artist.Id);
        }

        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult PutArtist(int id, Artist artist)
        {
            this._artistRepository.Update(p => p.Id == id, p => new Artist { Name = artist.Name, Seq = artist.Seq, ImageId = artist.ImageId, ShowOnHompage = artist.ShowOnHompage });
            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteArtist(int id)
        {
            _artistRepository.DeleteRange(p => p.Id == id);
            return Ok();
        }

        [Route("{id}/photo")]
        public async Task<HttpResponseMessage> GetPhotos(int id)
        {
            var currentHosting = _webHelper.GetStoreHost(_webHelper.IsCurrentConnectionSecured()).TrimEnd('/');
            var photos = await _galleryPhotoRepository.Table.AsNoTracking().Where(p => p.ArtistId == id).ToListAsync();
            var result = photos.Select(p =>
            {
                return new { p.Id, Url = currentHosting + _mediaService.GetPictureUrl(p.FileId) };
            });
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
