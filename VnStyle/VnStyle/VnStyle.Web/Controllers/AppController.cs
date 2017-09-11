using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VnStyle.Web.Controllers
{
    public class AppController : Controller
    {
        // GET: App
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Restart()
        {
            System.Web.HttpRuntime.UnloadAppDomain();
            return Redirect("/");
        }
    }
}