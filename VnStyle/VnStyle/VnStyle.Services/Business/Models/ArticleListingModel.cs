﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VnStyle.Services.Business.Models
{
    public class ArticleListingModel
    {
        public int Id { get; set; }
        public string HeadLine { get; set; }        
        public string UrlImage { get; set; }
        public string Extract { get; set; }
        public long? ImageId { get; set; }
        public DateTime PushlishDate { get; set; }
    }

    
}
