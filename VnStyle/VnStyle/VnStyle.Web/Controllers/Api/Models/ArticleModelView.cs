using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VnStyle.Services.Data.Enum;

namespace VnStyle.Web.Controllers.Api.Models
{
    public class ArticleModelView
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime PublishDate { get; set; }
        public int CreatedBy { get; set; }
        public EArticleState State { get; set; }
        public string HeadLine { get; set; }
        public long? FeatureImageId { get; set; }
        public ImageModelView FeatureImage { get; set; }

        public virtual IEnumerable<ArticleLanguageModelView> ArticleLanguages { get; set; }
    }
}