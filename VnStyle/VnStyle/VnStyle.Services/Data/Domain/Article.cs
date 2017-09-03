using System;
using System.Collections.Generic;
using VnStyle.Services.Data.Enum;

namespace VnStyle.Services.Data.Domain
{
    public class Article
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime PublishDate { get; set; }
        public int CreatedBy { get; set; }
        public EArticleState State { get; set; }
        public string HeadLine { get; set; }
        public long? FeatureImageId { get; set; }

        public virtual ICollection<ArticleLanguage> ArticleLanguages { get; set; }
    }
}
