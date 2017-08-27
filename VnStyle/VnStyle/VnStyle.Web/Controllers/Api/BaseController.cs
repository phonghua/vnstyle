using System.Web.Http;
using System.Web.Http.Cors;

namespace VnStyle.Web.Controllers.Api
{
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BaseController : ApiController
    {
    }
}
