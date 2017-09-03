using VnStyle.Services.Data.Enum;

namespace VnStyle.Services.Business.Messages
{
    public class UploadFileRequest
    {
        public byte[] Binary { get; set; }
        public string StoragePath { get; set; }
        public string MimeType { get; set; }
        public string Path { get; set; } // Path (length: 2047)
        public EMediaFileSourceTarget SourceTarget { get; set; } // SourceTarget
        
    }
}
