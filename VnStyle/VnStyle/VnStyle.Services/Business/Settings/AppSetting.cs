using Ricky.Infrastructure.Core.Configuration;

namespace VnStyle.Services.Business.Settings
{
    public class AppSetting  : ISetting
    {
        public string ApplicationName { get; set; }
        public string HomepageContact { get; set; }

        public string Facebook { get; set; }
        public string Youtube { get; set; }
        public string Instagram { get; set; }

    }
}
