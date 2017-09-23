using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VnStyle.Services.Business.Models;


namespace VnStyle.Services.Business
{
    public interface IArtistsService
    {
        IEnumerable<ArtistListingModel> GetAllArtists();
        IEnumerable<ImagesByArtist> GetAllImageByArtist(int id);
    }
}
