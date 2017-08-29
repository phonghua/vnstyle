namespace VnStyle.Services.Data.Domain
{
    public class ArticleLanguage
    {
        public long Id { get; set; }
        public string HeadLine { get; set; }
        public string Content { get; set; }
        public string Extract { get; set; }

        public string LanguageId { get; set; }
        

        public int MetaTagId { get; set; }
        public virtual MetaTag MetaTag { get; set; }

        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }

    }
}