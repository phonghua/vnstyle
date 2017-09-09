using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Ricky.Infrastructure.Core;
using Ricky.Infrastructure.Core.Caching;
using VnStyle.Services.Business.Dtos;
using VnStyle.Services.Business.Models;
using VnStyle.Services.Data;
using VnStyle.Services.Data.Enum;
using File = VnStyle.Services.Data.Domain.File;

namespace VnStyle.Services.Business
{
    public class MediaService : IMediaService
    {
        private readonly IBaseRepository<File> _fileRepository;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IWebHelper _webHelper;
        private readonly bool cacheNull = false;

        public MediaService(IBaseRepository<File> fileRepository, ICacheManager cacheManager, IWorkContext workContext, IWebHelper webHelper)
        {
            _fileRepository = fileRepository;
            _cacheManager = cacheManager;
            _workContext = workContext;
            _webHelper = webHelper;
        }

        public long InsertPicture(UploadFileRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var file = new File
            {
                MimeType = request.MimeType,
                SourceTarget = (int)request.SourceTarget
            };
            switch (request.SourceTarget)
            {
                case EMediaFileSourceTarget.ImageDisk:
                case EMediaFileSourceTarget.VideoDisk:
                    {
                        //file.Path

                        var filePath = Path.Combine(_webHelper.MapPath("~/"), request.StoragePath, request.Path.Trim('\\'));
                        var folder = Path.GetDirectoryName(filePath);
                        if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                        file.Path = Path.Combine("/", request.StoragePath, request.Path.Trim('\\')).Replace('\\', '/');
                        System.IO.File.WriteAllBytes(filePath, request.Binary);
                        break;
                    }
                case EMediaFileSourceTarget.ExtSite:
                case EMediaFileSourceTarget.Youtube:
                    {
                        file.Path = request.Path;
                        break;
                    }
            }
            _fileRepository.Insert(file);
            _fileRepository.SaveChanges();

            _cacheManager.Remove(string.Format(CachingKey.FileServiceById, file.Id));
            return file.Id;
        }

        public string GetPictureUrl(long pictureId)
        {
            var fileDto = GetFileDto(pictureId);
            return fileDto?.Path;
        }
        private FileDto GetFileDto(long fileId)
        {
            var fileDto = _cacheManager.Get(string.Format(CachingKey.FileServiceById, fileId), CachingKey.FileServiceCacheTime, () =>
            {
                return _fileRepository.Table.Where(p => p.Id == fileId).ProjectTo<FileDto>().FirstOrDefault();
            }, cacheNull);
            return fileDto;
        }

        public List<FileDto> GetPictureUrl(List<long> pictureId)
        {
            return pictureId.Select(GetFileDto).ToList();
        }
    }
}