using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VnStyle.Web.Controllers.Api.Models
{
    public class UploadPhotoModelView
    {
        public long Id { get; set; }
        public long PhotoId { get; set; }
        public string FilePath { get; set; }
        public string FileUrl { get; set; }
    }
}