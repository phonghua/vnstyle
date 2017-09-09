using System.Collections.Generic;
using VnStyle.Services.Business.Dtos;
using VnStyle.Services.Business.Models;

namespace VnStyle.Services.Business
{
    public interface IMediaService
    {
        long InsertPicture(UploadFileRequest request);
        string GetPictureUrl(long pictureId);
        List<FileDto> GetPictureUrl(List<long> pictureId);
    }
}