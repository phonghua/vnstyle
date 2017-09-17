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
                    HomepageContact = "+84 939293186 (Phạm Mai)",
                    Facebook = "https://www.facebook.com/vnstyletattoo",
                    Youtube = "https://www.youtube.com/",
                    Instagram = "...."
                };

                settingService.SaveConfiguration(appsetting);

                settingService.SetConfiguration("initialized", true);
            }
        }
    }
}