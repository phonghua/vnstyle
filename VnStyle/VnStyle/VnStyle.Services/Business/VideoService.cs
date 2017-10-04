using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ricky.Infrastructure.Core;
using Ricky.Infrastructure.Core.Generic;
using VnStyle.Services.Business.Models;
using VnStyle.Services.Data;
using VnStyle.Services.Data.Domain;
namespace VnStyle.Services.Business
{
    public class VideoService : IVideoService
    {
        private readonly IBaseRepository<Movie> _movieRepository;
        public VideoService(IBaseRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }
        //public IList<VideoListingModel> GetVideoThumb()
        //{
            
        //    return _movieRepository.Table.OrderByDescending(p => p.Id).Select(p => new VideoListingModel { Id = p.Id, Link = p.Link , Title = p.Title}).ToList();
        //}

        public IPagedList<VideoListingModel> GetVideoThumb(PagingRequest request)
        {
            
            if (request.PageIndex < 0) request.PageIndex = 0;
            if (request.PageSize < 1) request.PageSize = 10;
            var movie = _movieRepository.Table.OrderByDescending(p => p.Id).Select(p => new VideoListingModel { Id = p.Id, Link = p.Link, Title = p.Title });
            var pageMovie = movie.Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToList();
            var count = movie.Count();
            return new PagedList<VideoListingModel>(pageMovie, request.PageIndex, request.PageSize, count);

        }
    }
}
