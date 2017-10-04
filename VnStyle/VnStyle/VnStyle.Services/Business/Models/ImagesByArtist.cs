using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VnStyle.Services.Business.Models
{
    public class ImagesByArtist
    {
        public int IdArtist { get; set; }
        public string Name { get; set; }
        public long ImageId { get; set; }
        public string UrlImage { get; set; }
        public int Count { get; set; }
    }
    //public List MyProperty { get; set; }
}
