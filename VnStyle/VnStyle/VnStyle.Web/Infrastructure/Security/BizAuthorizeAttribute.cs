using System.Web.Http.Controllers;
using Ricky.Infrastructure.Core;
using Ricky.Infrastructure.Core.ObjectContainer;

namespace VnStyle.Web.Infrastructure.Security
{
    public class BizAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        private readonly IWorkContext _workContext;

        public BizAuthorizeAttribute()
        {
            _workContext = EngineContext.Current.Resolve<IWorkContext>();
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            //if (!HttpContext.Current.User.Identity.IsAuthenticated)
            //{
            //    base.HandleUnauthorizedRequest(actionContext);
            //}
            //else
            //{
            //    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            //}

            if (!_workContext.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                //actionContext.Response = new HttpResponseMessage();
            }
        }
    }
}