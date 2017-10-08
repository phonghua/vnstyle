using System;

namespace VnStyle.Web.Models.Home
{
    public class SiteMetaTag
    {
        public string CurrentUrl { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }

        public string Image { get; set; }
        public DateTime ContentCreatedDate { get; set; }
        public string Publisher { get; set; }

    }
}