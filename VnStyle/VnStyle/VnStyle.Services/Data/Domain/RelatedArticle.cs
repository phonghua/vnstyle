namespace VnStyle.Services.Data.Domain
{
    public class RelatedArticle
    {
        public long Id { get; set; }
        public int Article1Id { get; set; }
        public int Article2Id { get; set; }

        public virtual Article Article1 { get; set; }
        public virtual Article Article2 { get; set; }

    }
}