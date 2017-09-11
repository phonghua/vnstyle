using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VnStyle.Services.Data.Enum;

namespace VnStyle.Services.Business.Models
{
    public class ArticleModelRequest
    {
        public int rootCate { get; set; }
        public string currentLanguage { get; set; }
        public string defaultLanguage { get; set; }
    }
}
