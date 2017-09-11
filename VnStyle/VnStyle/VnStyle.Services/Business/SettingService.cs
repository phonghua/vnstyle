using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Ricky.Infrastructure.Core;
using Ricky.Infrastructure.Core.Caching;
using Ricky.Infrastructure.Core.Configuration;
using VnStyle.Services.Data;
using VnStyle.Services.Data.Domain;

namespace VnStyle.Services.Business
{
    public class SettingService : ISettingService
    {

        private readonly ICacheManager _cacheManager;
        private readonly IBaseRepository<Setting> _settingRepository;

        public SettingService(ICacheManager cacheManager, IBaseRepository<Setting> settingRepository)
        {
            _cacheManager = cacheManager;
            _settingRepository = settingRepository;
        }

        #region Nested classes

        [Serializable]
        public class SettingForCaching
        {
            public int Id { get; set; }
            public string Key { get; set; }
            public string Value { get; set; }
        }

        #endregion

        public void DeleteConfiguration(long configurationId)
        {
            _settingRepository.DeleteRange(p => p.Id == configurationId);
            _cacheManager.RemoveByPattern(CachingKey.SettingServicePattern);
        }

        public T GetConfigurationByKey<T>(string key, T defaultValue = default(T))
        {
            if (String.IsNullOrEmpty(key))
                return defaultValue;

            var settings = GetAllSettingsCached();
            key = key.Trim().ToLowerInvariant();
            if (settings.ContainsKey(key))
            {
                var settingsByKey = settings[key];
                var setting = settingsByKey.FirstOrDefault();

                if (setting != null) return CommonHelper.To<T>(setting.Value);
            }
            return defaultValue;
        }

        public void SetConfiguration<T>(string key, T value, bool clearCache = true)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            key = key.Trim().ToLowerInvariant();
            string valueStr = CommonHelper.GetCustomTypeConverter(typeof(T)).ConvertToInvariantString(value);

            var allSettings = GetAllSettingsCached();
            var settingForCaching = allSettings.ContainsKey(key) ?
                allSettings[key].FirstOrDefault() : null;
            if (settingForCaching != null)
            {
                //update
                //var setting = ConfigurationRepository.GetById(settingForCaching.Id);
                //setting.Value = valueStr;
                //DbContext.SaveChanges();


                var oldSetting = _settingRepository.GetById(settingForCaching.Id);
                //DbContext.Set<DAL.Models.Configuration>().Attach(oldSetting);
                oldSetting.Value = valueStr;
                _settingRepository.SaveChanges();

            }
            else
            {
                var settingObj = new Setting
                {
                    Key = key,
                    Value = valueStr,
                };
                _settingRepository.Insert(settingObj);
                _settingRepository.SaveChanges();
            }
            if (clearCache) _cacheManager.RemoveByPattern(CachingKey.SettingServicePattern);
        }

        public IList<Setting> GetAllConfigurations()
        {
            var settings = _settingRepository.Table.ToList();
            return settings;
        }

        public bool ConfigurationExists<T, TPropType>(T configurations, Expression<Func<T, TPropType>> keySelector) where T : ISetting, new()
        {
            var member = keySelector.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    keySelector));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    keySelector));
            }

            string key = typeof(T).Name + "." + propInfo.Name;

            string setting = GetConfigurationByKey<string>(key);
            return setting != null;
        }

        public T LoadConfiguration<T>() where T : ISetting, new()
        {
            var settings = Activator.CreateInstance<T>();

            foreach (var prop in typeof(T).GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                var key = typeof(T).Name + "." + prop.Name;
                //load by store
                string setting = GetConfigurationByKey<string>(key);
                if (setting == null) continue;
                if (!CommonHelper.GetCustomTypeConverter(prop.PropertyType).CanConvertFrom(typeof(string))) continue;
                if (!CommonHelper.GetCustomTypeConverter(prop.PropertyType).IsValid(setting)) continue;
                object value = CommonHelper.GetCustomTypeConverter(prop.PropertyType).ConvertFromInvariantString(setting);

                //set property
                prop.SetValue(settings, value, null);
            }

            return settings;
        }

        public void SaveConfiguration<T>(T configurations) where T : ISetting, new()
        {
            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            foreach (var prop in typeof(T).GetProperties())
            {
                // get properties we can read and write to
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                if (!CommonHelper.GetCustomTypeConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                string key = typeof(T).Name + "." + prop.Name;
                //Duck typing is not supported in C#. That'_settingRepository why we're using dynamic type
                dynamic value = prop.GetValue(configurations, null);
                if (value != null)
                    SetConfiguration(key, value, false);
                else
                    SetConfiguration(key, string.Empty, false);
            }

            //and now clear cache
            ClearCache();
        }

        public void SaveConfiguration<T, TPropType>(T configurations, Expression<Func<T, TPropType>> keySelector, bool clearCache = true) where T : ISetting, new()
        {
            var member = keySelector.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    keySelector));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    keySelector));
            }

            string key = typeof(T).Name + "." + propInfo.Name;
            //Duck typing is not supported in C#. That'_settingRepository why we're using dynamic type
            dynamic value = propInfo.GetValue(configurations, null);
            if (value != null)
                SetConfiguration(key, value, clearCache);
            else
                SetConfiguration(key, "", clearCache);
        }

        public void DeleteConfiguration<T>() where T : ISetting, new()
        {
            var settingsToDelete = new List<int>();
            var allSettings = GetAllConfigurations();
            foreach (var prop in typeof(T).GetProperties())
            {
                string key = typeof(T).Name + "." + prop.Name;
                settingsToDelete.AddRange(allSettings.Where(x => x.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)).Select(p => p.Id));
            }

            if (settingsToDelete.Any())
                _settingRepository.DeleteRange(p => settingsToDelete.Contains(p.Id));

            ClearCache();
        }

        public void DeleteConfiguration<T, TPropType>(T configurations, Expression<Func<T, TPropType>> keySelector) where T : ISetting, new()
        {
            var member = keySelector.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format("Expression '{0}' refers to a method, not a property.", keySelector));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format("Expression '{0}' refers to a field, not a property.", keySelector));
            }

            string key = typeof(T).Name + "." + propInfo.Name;
            key = key.Trim().ToLowerInvariant();

            var allSettings = GetAllSettingsCached();
            var settingForCaching = allSettings.ContainsKey(key) ?
                allSettings[key].FirstOrDefault() : null;
            if (settingForCaching != null)
            {
                //var setting = ConfigurationRepository.GetById(settingForCaching.Id);
                ////DbContext.Set<DAL.Models.Configuration>().Attach(setting);

                //setting.IsDeleted = true;
                //DbContext.SaveChanges();
                _settingRepository.DeleteRange(p => p.Id == settingForCaching.Id);
                _settingRepository.SaveChanges();
                ClearCache();
            }
        }

        #region "Private functions"
        protected virtual IDictionary<string, IList<SettingForCaching>> GetAllSettingsCached()
        {
            string key = string.Format(CachingKey.SettingServicePattern);
            return _cacheManager.Get(key, () =>
            {

                var settings = _settingRepository.GetAll().ToList();

                var dictionary = new Dictionary<string, IList<SettingForCaching>>();

                foreach (var s in settings)
                {
                    var resourceName = s.Key.ToLowerInvariant();
                    var settingForCaching = new SettingForCaching()
                    {
                        Id = s.Id,
                        Key = s.Key,
                        Value = s.Value
                    };
                    if (!dictionary.ContainsKey(resourceName))
                    {
                        //first setting
                        dictionary.Add(resourceName, new List<SettingForCaching>()
                        {
                            settingForCaching
                        });
                    }
                    else
                    {
                        //already added
                        //most probably it'_settingRepository the setting with the same name but for some certain store (storeId > 0)
                        dictionary[resourceName].Add(settingForCaching);
                    }
                }
                return dictionary;
            });
        }

        private void ClearCache()
        {
            _cacheManager.RemoveByPattern(CachingKey.SettingServicePattern);
        }
        #endregion

        public void Dispose()
        {
        }
    }
}