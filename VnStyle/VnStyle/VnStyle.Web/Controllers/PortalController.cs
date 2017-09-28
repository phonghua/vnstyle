using System.Web.Mvc;
using Newtonsoft.Json;
using VnStyle.Web.Infrastructure.Helpers;

namespace VnStyle.Web.Controllers
{
    public class PortalController : Controller
    {
        // GET: Portal
        public ActionResult Index()
        {
            ViewBag.Settings = JsonConvert.SerializeObject(new
            {
                portal = Url.BaseUrl()
            });

            return View();
        }
    }
}