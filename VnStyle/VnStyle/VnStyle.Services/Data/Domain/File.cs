namespace VnStyle.Services.Data.Domain
{
    public class File
    {
        public long Id { get; set; }
        public string MimeType { get; set; }
        public string Path { get; set; }
        public int SourceTarget { get; set; }
        public bool IsUsed { get; set; }
    }
}