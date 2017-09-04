using System.Collections.Generic;
using VnStyle.Services.Data.Domain;

namespace VnStyle.Services.Business
{
    public interface IResourceService
    {
        List<Language> GetLanguages();
        string T(string text, params object[] arg);
        string DefaultLanguageId();
    }
}
