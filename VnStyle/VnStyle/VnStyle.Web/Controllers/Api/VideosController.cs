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
    [RoutePrefix("api/videos")]
    public class VideosController : BaseController
    {
        #region "Fields and Properties"
        private readonly IBaseRepository<Movie> _movieRespository;

        public VideosController()
        {
            _movieRespository = EngineContext.Current.Resolve<IBaseRepository<Movie>>();
        }

        #endregion


        [Route("")]

        public async Task<HttpResponseMessage> GetVideos()
        {
            var movies = await _movieRespository.Table.ToListAsync();
            return Request.CreateResponse(HttpStatusCode.OK, movies);
        }

        [Route("")]
        [HttpPost]
        public  HttpResponseMessage PostVideo(Movie movie)
        {
            _movieRespository.Insert(movie);
            _movieRespository.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("")]
        [HttpPut]
        public HttpResponseMessage PutVideo(Movie movie)
        {
            _movieRespository.Insert(movie);
            _movieRespository.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("{id}")]
        [HttpDelete]
        public HttpResponseMessage DeleteVideo(int id)
        {
            _movieRespository.DeleteRange(p => p.Id == id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
