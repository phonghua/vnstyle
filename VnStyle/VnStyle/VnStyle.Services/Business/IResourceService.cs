using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ServiceStack.Common;
using VnStyle.Services.Data;
using VnStyle.Services.Data.Domain;

namespace VnStyle.Services.Business
{
    public interface IResourceService
    {
        List<Language> GetLanguages();
        string T(string text, params object[] arg);
    }
}
