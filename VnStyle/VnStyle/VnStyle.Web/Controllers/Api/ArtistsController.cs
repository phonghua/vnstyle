using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
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

        public ArtistsController()
        {
            _artistRepository = EngineContext.Current.Resolve<IBaseRepository<Artist>>();
            _resourceService = EngineContext.Current.Resolve<IResourceService>();
        }

        #endregion

        [Route("")]
        public async Task<IHttpActionResult> GetArtists()
        {
            return Ok(await _artistRepository.Table.OrderBy(p => p.Seq).ToListAsync());

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
        [HttpPost]
        public IHttpActionResult PutArtist(int id, Artist artist)
        {
            this._artistRepository.Update(p => p.Id == id, p => new Artist { Name = artist.Name, Seq = artist.Seq});
            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteArtist(int id)
        {
            _artistRepository.DeleteRange(p => p.Id == id);
            return Ok();
        }
    }
}
