using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VnStyle.Services.Business.Models
{
    public class ArtistListingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Seq { get; set; }
        public long? ImageId { get; set; }        
        public string UrlImage { get; set; }
    }
}
