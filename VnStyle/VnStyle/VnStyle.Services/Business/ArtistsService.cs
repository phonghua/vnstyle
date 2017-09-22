using Ricky.Infrastructure.Core;
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
    public class ArtistsService : IArtistsService

    {
        private readonly IBaseRepository<Artist> _artistRepository;
        
      
        private readonly IMediaService _mediaService;
        public ArtistsService(IBaseRepository<Artist> artistRepository, IMediaService mediaService)
        {
            _artistRepository = artistRepository;
            _mediaService = mediaService;
        }
        public IEnumerable<ArtistListingModel> GetAllArtists()
        {
            
            var query = _artistRepository.Table.Where(p => p.ShowOnHompage == true).OrderBy(p => p.Seq);
            if (query == null)
            {
                return null;
            }
                    
            var artists = query.Select(p => new ArtistListingModel
            {
                Id = p.Id,
                Name = p.Name,
                ImageId = p.ImageId
            }).ToList();
            foreach (var artist in artists)
            {
                if (artist.ImageId.HasValue)
                    artist.UrlImage = _mediaService.GetPictureUrl(artist.ImageId.Value);
                else
                    artist.UrlImage = "~/Content/images/no-image.png";
            }
            return artists;

        }
    }
}
