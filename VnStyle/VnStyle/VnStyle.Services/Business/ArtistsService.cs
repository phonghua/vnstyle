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
        private readonly IBaseRepository<GalleryPhoto> _galleryPhoto;


        private readonly IMediaService _mediaService;
        public ArtistsService(IBaseRepository<Artist> artistRepository, IMediaService mediaService, IBaseRepository<GalleryPhoto> galleryPhoto)
        {
            _artistRepository = artistRepository;
            _mediaService = mediaService;
            _galleryPhoto = galleryPhoto;
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

        public IEnumerable<ImagesByArtist> GetAllImageByArtist(int id)
        {

            var query = _galleryPhoto.Table.Where(p => p.ArtistId == id);
            var queryJoin = (from g in query
                            join a in _artistRepository.Table on g.ArtistId equals a.Id
                            select new ImagesByArtist
                            {
                                Name = a.Name,
                                ImageId = g.FileId
                            }).ToList();


            //var images = (from g in _galleryPhoto.Table.Where(p => p.Id == id)
            //              join a in _artistRepository.Table on g.ArtistId equals a.Id
            //              select new ImagesByArtist
            //              {
            //                  Name = a.Name,
            //                  ImageId = g.FileId
            //              }).ToList();



            foreach (var image in queryJoin)
            {
                image.UrlImage = _mediaService.GetPictureUrl(image.ImageId);
            }
            return queryJoin;

            //var a = _galleryPhoto.Table.Where(p => p.ArtistId == id);
            


        }
    }
}
