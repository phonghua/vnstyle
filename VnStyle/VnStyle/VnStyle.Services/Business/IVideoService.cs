using Ricky.Infrastructure.Core;
using Ricky.Infrastructure.Core.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VnStyle.Services.Business.Models;

namespace VnStyle.Services.Business
{
    public interface IVideoService
    {
        IPagedList<VideoListingModel> GetVideoThumb(PagingRequest request);
        VideoListingModel GetVideoById(int id);
        List<VideoListingModel> GetRelatedVideo();
    }
}
