using System;

namespace VnStyle.Services.Data.Domain
{
    public class GalleryPhoto
    {
        public int Id { get; set; }
        public long FileId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ArtistId { get; set; }
    }
}