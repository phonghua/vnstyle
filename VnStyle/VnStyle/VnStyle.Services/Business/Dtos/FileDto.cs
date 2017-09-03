namespace VnStyle.Services.Business.Dtos
{
    public class FileDto
    {
        public long Id { get; set; }
        public string MimeType { get; set; }
        public string Path { get; set; }
        public int SourceTarget { get; set; }
    }
}