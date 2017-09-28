using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public IList<VideoListingModel> GetVideoThumb()
        {
            
            return _movieRepository.Table.Select(p => new VideoListingModel { Id = p.Id, Link = p.Link , Title = p.Title}).ToList();
        }
    }
}
