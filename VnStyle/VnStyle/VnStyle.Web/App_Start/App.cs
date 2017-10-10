using Ricky.Infrastructure.Core.ObjectContainer;
using VnStyle.Services.Business;
using VnStyle.Services.Business.Settings;

namespace VnStyle.Web
{
    public static class App
    {
        public static void Intialize()
        {
            var settingService = EngineContext.Current.Resolve<ISettingService>();
            var initialized = settingService.GetConfigurationByKey<bool>("initialized");
            if (!initialized)
            {
                var appsetting = new AppSetting
                {
                    ApplicationName = "VNStyle Tattoo & Piercing",
                    HomepageContact1 = "+84 939293186 (Phạm Mai)",
                    HomepageContact2 = "+84 986666149  (Đăng Vinh)",
                    Facebook = "https://www.facebook.com/vnstyletattoo",
                    Youtube = "https://www.youtube.com/channel/UCfazwxHB6usZ4FOa2-LFaBQ/",
                    Instagram = "https://www.instagram.com/explore/locations/299835778/vnstyle-tattoo-piercing/",
                    
                    FbAppSecret = "6efa00c7957b1908f99ac622e1bbb21f",
                    FbAppId = "132801417343488"
                };

                settingService.SaveConfiguration(appsetting);

                settingService.SetConfiguration("initialized", true);
            }
        }
    }
}