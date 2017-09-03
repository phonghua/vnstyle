namespace VnStyle.Services.Data.Domain
{
    public class ArticleCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Parent { get; set; }
        public int Seq { get; set; }
        public bool InActive { get; set; }
    }
}