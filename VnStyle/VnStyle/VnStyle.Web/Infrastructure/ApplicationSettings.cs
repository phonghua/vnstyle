namespace VnStyle.Web.Infrastructure
{
    public static class ApplicationSettings
    {
        public const string ImageStoragePath = "contents/uploads/images";
        public const string VideoStoragePath = "contents/uploads/videos";
        public const int ImageSizeNoneResize = 0;
        public const int ImageSizeSmallSize = 320;
        public const int ImageSizeMediumSize = 480;
        public const int ImageSizeLargeSize = 1024;

        public const string NoImagePath = "/contents/images/no-images.png";
    }
}