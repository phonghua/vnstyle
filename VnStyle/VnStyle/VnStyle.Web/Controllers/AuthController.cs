using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Ricky.Infrastructure.Core;
using Ricky.Infrastructure.Core.ObjectContainer;
using VnStyle.Services.Business;
using VnStyle.Web.Infrastructure.Security;
using VnStyle.Web.Models.Auth;
using Microsoft.AspNet.Identity.Owin;

namespace VnStyle.Web.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        public UserManager<ApplicationUser, int> UserManager { get; private set; }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private ApplicationSignInManager _signInManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {

                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        private readonly IAppContextUrlRouting _routing;

        public AuthController()
        {
            var userService = EngineContext.Current.Resolve<IUserService>();
            UserManager = new UserManager<ApplicationUser, int>(new UserStore<ApplicationUser>(userService));
            _routing = EngineContext.Current.Resolve<IAppContextUrlRouting>();
        }

        // GET: FE/Auth
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            var user = UserManager.FindByName("phong.hd1989@gmail.com");
            if (user == null) UserManager.Create(new ApplicationUser { Email = "phong.hd1989@gmail.com", UserName = "phong.hd1989@gmail.com", EmailConfirmed = true }, "ADmin@123");
            


           

            ViewBag.ReturnUrl = returnUrl;
            var model = new LoginRequest();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginRequest model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    //await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Email hoặc mật khẩu không hợp lệ");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect(_routing.Home());
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();

            if (Request.IsAjaxRequest())
            {
                return Json(new { });
            }
            return RedirectToLocal(_routing.Home());
        }
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;

        //public AuthController()
        //{
        //}

        //public AuthController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        //{
        //    UserManager = userManager;
        //    SignInManager = signInManager;
        //}

        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        //    }
        //    private set
        //    {
        //        _signInManager = value;
        //    }
        //}

        //public ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //    private set
        //    {
        //        _userManager = value;
        //    }
        //}

        ////
        //// GET: /Account/Login
        //[AllowAnonymous]
        //public ActionResult Login(string returnUrl)
        //{
        //    ViewBag.ReturnUrl = returnUrl;
        //    return View();
        //}

        ////
        //// POST: /Account/Login
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // This doesn't count login failures towards account lockout
        //    // To enable password failures to trigger account lockout, change to shouldLockout: true
        //    var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(returnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
        //        case SignInStatus.Failure:
        //        default:
        //            ModelState.AddModelError("", "Invalid login attempt.");
        //            return View(model);
        //    }
        //}
    }
}