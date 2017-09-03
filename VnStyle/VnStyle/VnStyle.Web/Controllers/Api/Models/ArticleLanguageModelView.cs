using VnStyle.Services.Data.Domain;

namespace VnStyle.Web.Controllers.Api.Models
{
    public class ArticleLanguageModelView
    {
        public long Id { get; set; }
        public string HeadLine { get; set; }
        public string Content { get; set; }
        public string Extract { get; set; }

        public string LanguageId { get; set; }
        public int MetaTagId { get; set; }
        public virtual MetaTag MetaTag { get; set; }
    }

    
}