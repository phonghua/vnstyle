using System.Web.Mvc;

namespace VnStyle.Web.Controllers
{
    public class BaseController : Controller
    {
        public ActionResult NotFound()
        {
            return View("_NotFound");
        }
    }
}