using VnStyle.Services.Data.Enum;

namespace VnStyle.Services.Data.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Parent { get; set; }
        public int Seq { get; set; }
        public bool InActive { get; set; }
        public long? FeaturedImageId { get; set; }
        public ERootCategory RootCategory { get; set; }
        
    }
}