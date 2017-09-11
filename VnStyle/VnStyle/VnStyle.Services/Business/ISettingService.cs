using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Ricky.Infrastructure.Core.Configuration;
using VnStyle.Services.Data.Domain;

namespace VnStyle.Services.Business
{
    public interface ISettingService
    {
        void DeleteConfiguration(long configurationId);

        T GetConfigurationByKey<T>(string key, T defaultValue = default(T));

        void SetConfiguration<T>(string key, T value, bool clearCache = true);

        IList<Setting> GetAllConfigurations();

        bool ConfigurationExists<T, TPropType>(T configurations, Expression<Func<T, TPropType>> keySelector)
            where T : ISetting, new();

        T LoadConfiguration<T>() where T : ISetting, new();

        void SaveConfiguration<T>(T configurations) where T : ISetting, new();

        void SaveConfiguration<T, TPropType>(T configurations, Expression<Func<T, TPropType>> keySelector,
            bool clearCache = true) where T : ISetting, new();

        void DeleteConfiguration<T>() where T : ISetting, new();

        void DeleteConfiguration<T, TPropType>(T configurations, Expression<Func<T, TPropType>> keySelector)
            where T : ISetting, new();
    }
}