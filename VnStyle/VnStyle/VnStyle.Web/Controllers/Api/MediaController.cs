using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Ricky.Infrastructure.Core;
using Ricky.Infrastructure.Core.ObjectContainer;
using VnStyle.Services.Business;
using VnStyle.Services.Business.Models;
using VnStyle.Services.Data;
using VnStyle.Services.Data.Domain;
using VnStyle.Services.Data.Enum;
using VnStyle.Web.Controllers.Api.Models;
using VnStyle.Web.Infrastructure;

namespace VnStyle.Web.Controllers.Api
{
    [RoutePrefix("api/media")]
    public class MediaController : BaseController
    {
        #region "Fields and Properties"

        private readonly IMediaService _mediaService;
        private readonly IWebHelper _webHelper;
        private readonly IBaseRepository<GalleryPhoto> _galleryPhotoRepository;


        #endregion

        #region "Constructor"
        public MediaController()
        {
            _mediaService = EngineContext.Current.Resolve<IMediaService>();
            _webHelper = EngineContext.Current.Resolve<IWebHelper>();
            _galleryPhotoRepository = EngineContext.Current.Resolve<IBaseRepository<GalleryPhoto>>();
        }
        #endregion

        [HttpPost]
        [Route("photo")]
        public HttpResponseMessage UploadPhoto()
        {
            var currentHosting = _webHelper.GetStoreHost(_webHelper.IsCurrentConnectionSecured()).TrimEnd('/');

            List<UploadPhotoModelView> images = new List<UploadPhotoModelView>();
            int fileCount = HttpContext.Current.Request.Files.Count;
            for (int i = 0; i < fileCount; i++)
            {
                HttpPostedFile file = HttpContext.Current.Request.Files[i];
                var fileName = Path.GetFileName(file.FileName);

                var data = StreamHelper.ReadToEnd(file.InputStream);
                var pictureId = _mediaService.InsertPicture(new UploadFileRequest
                {
                    SourceTarget = EMediaFileSourceTarget.ImageDisk,
                    Binary = data,
                    MimeType = file.ContentType,
                    StoragePath = ApplicationSettings.ImageStoragePath,
                    Path = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}"
                });
                var imageUrl = _mediaService.GetPictureUrl(pictureId);
                images.Add(new UploadPhotoModelView
                {
                    Id = pictureId,
                    FilePath = imageUrl,
                    FileUrl = $"{currentHosting}{imageUrl}",
                });
            }
            JsonDataResult.Data = new { images = images };
            return this.CreateResponseMessage();
        }


        [HttpPost]
        [Route("gallery-photo/{cateId}")]
        public HttpResponseMessage UploadAlbumPhoto(int cateId)
        {
            var currentHosting = _webHelper.GetStoreHost(_webHelper.IsCurrentConnectionSecured()).TrimEnd('/');

            List<UploadPhotoModelView> images = new List<UploadPhotoModelView>();
            int fileCount = HttpContext.Current.Request.Files.Count;
            for (int i = 0; i < fileCount; i++)
            {
                HttpPostedFile file = HttpContext.Current.Request.Files[i];
                var fileName = Path.GetFileName(file.FileName);

                var data = StreamHelper.ReadToEnd(file.InputStream);
                var pictureId = _mediaService.InsertPicture(new UploadFileRequest
                {
                    SourceTarget = EMediaFileSourceTarget.ImageDisk,
                    Binary = data,
                    MimeType = file.ContentType,
                    StoragePath = ApplicationSettings.ImageStoragePath,
                    Path = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}"
                });
                var imageUrl = _mediaService.GetPictureUrl(pictureId);
                images.Add(new UploadPhotoModelView
                {
                    Id = pictureId,
                    FilePath = imageUrl,
                    FileUrl = $"{currentHosting}{imageUrl}",
                });
            }

            // save to album
            images.ForEach(p =>
            {
                this._galleryPhotoRepository.Insert(new GalleryPhoto
                {
                    FileId = p.Id,
                    CategoryId = cateId,
                    CreatedDate = DateTimeHelper.GetCurrentDateTime()
                });
            });

            this._galleryPhotoRepository.SaveChanges();

            JsonDataResult.Data = new { images = images };
            return this.CreateResponseMessage();
        }
    }
}
