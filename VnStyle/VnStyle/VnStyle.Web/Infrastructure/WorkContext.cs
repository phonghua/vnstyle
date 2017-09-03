using System;
using System.Web;
using Microsoft.AspNet.Identity;
using Ricky.Infrastructure.Core;

namespace VnStyle.Web.Infrastructure
{
    public class WorkContext : IWorkContext
    {
        private readonly HttpContextBase _httpContext;

        public WorkContext()
        {
            var httpContextBase = new HttpContextWrapper(HttpContext.Current);
            this._httpContext = httpContextBase;
        }

        public bool IsAuthenticated { get { return _httpContext.User.Identity.IsAuthenticated; } }
        public int CurrentUserId
        {
            get
            {
                if (!IsAuthenticated) throw new UnauthorizedAccessException();
                return _httpContext.User.Identity.GetUserId<int>();
            }
        }
        public int? CurrentCompanyId { get; }
        public int? CurrentMarkupId { get; }
        public bool IsAuthorized(string permissionName)
        {
            throw new NotImplementedException();
        }

        public bool IsAuthorized(int userId, string permissionName)
        {
            throw new NotImplementedException();
        }

        public int Gmt { get; }
        public UserBaseInfo GetUserBaseInfo(int userId)
        {
            throw new NotImplementedException();
        }

        public string CurrentLanguage
        {
            get
            {
                if (_httpContext.Request.RequestContext.RouteData.Values["lang"] != null && _httpContext.Request.RequestContext.RouteData.Values["lang"] as string != "null") return _httpContext.Request.RequestContext.RouteData.Values["lang"] as string;
                return "vi";
            }
        }
    }
}