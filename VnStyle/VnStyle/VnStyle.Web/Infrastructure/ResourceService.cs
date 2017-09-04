using System.Collections.Generic;
using System.Linq;
using System.Resources;
using Ricky.Infrastructure.Core;
using Ricky.Infrastructure.Core.Caching;
using VnStyle.Services.Business;
using VnStyle.Services.Data.Domain;

namespace VnStyle.Web.Infrastructure
{
    public class ResourceService : IResourceService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;

        public ResourceService(ICacheManager cacheManager, IWorkContext workContext)
        {
            _cacheManager = cacheManager;
            _workContext = workContext;
        }

        public List<Language> GetLanguages()
        {
            return new List<Language>
            {
                new Language {Code = "vi", Id = "vi",IsDefault = true, Name = "Tiếng Việt"  },
                new Language {Code = "en",Id="en", IsDefault = false, Name = "English"},
            };
        }

        public string T(string text, params object[] arg)
        {
            System.Type resourceType = typeof(App_LocalResources.vi);
            if (_workContext.CurrentLanguage == "en") resourceType = typeof(App_LocalResources.en);
            ResourceManager dialogResources = new ResourceManager(resourceType);
            string myString = dialogResources.GetString(text);
            if (string.IsNullOrEmpty(myString)) return text;
            return string.Format(myString, arg);
        }

        public string DefaultLanguageId()
        {
            return GetLanguages().Where(p => p.IsDefault).Select(p => p.Id).FirstOrDefault();
        }
    }
}