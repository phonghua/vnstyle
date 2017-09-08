using System;

namespace VnStyle.Web.Models.Home
{
    public class ArticleViewerModelView
    {
        public int Id { get; set; }
        public string Headline { get; set; }
        public string Content { get; set; }
        public string UrlImage { get; set; }
        public string Extract { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}