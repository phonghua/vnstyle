using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Ricky.Infrastructure.Core.ObjectContainer;
using VnStyle.Services.Data;
using VnStyle.Services.Data.Domain.Memberships;
using VnStyle.Web.Controllers.Api.Models;
using VnStyle.Web.Infrastructure.Security;
using Microsoft.AspNet.Identity.Owin;
using Ricky.Infrastructure.Core;
using VnStyle.Services.Business;
using VnStyle.Web.Infrastructure;

namespace VnStyle.Web.Controllers.Api
{
    [RoutePrefix("api/users")]
    public class UsersController : BaseController
    {
        private readonly IBaseRepository<AspNetUser> _userRepository;
        private readonly IWorkContext _workContext;

        public UserManager<ApplicationUser, int> UserManager { get; private set; }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }
        private ApplicationSignInManager _signInManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {

                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        public UsersController()
        {
            _userRepository = EngineContext.Current.Resolve<IBaseRepository<AspNetUser>>();
            var userService = EngineContext.Current.Resolve<IUserService>();
            UserManager = new UserManager<ApplicationUser, int>(new UserStore<ApplicationUser>(userService));
            _workContext = EngineContext.Current.Resolve<IWorkContext>();
        }

        [Route("")]
        public async Task<HttpResponseMessage> GetUsers()
        {
            var except = new List<string>
            {
                "portal",
                "phong.hd1989@gmail.com"
            };
            var users = await _userRepository.Table.AsNoTracking().Where(p => !except.Contains(p.Email)).Select(p => new { p.Id, p.Email, p.UserName }).ToListAsync();
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }

        [Route("")]
        [HttpPost]
        public HttpResponseMessage CreateUser(UserModel user)
        {
            var userEntity = UserManager.FindByName(user.Email);
            if (userEntity == null)
            {
                UserManager.Create(new ApplicationUser { Email = user.Email, UserName = user.Email, EmailConfirmed = true }, user.Password);
                userEntity = UserManager.FindByName(user.Email);
                UserManager.AddPasswordAsync(userEntity.Id, user.Password).Wait();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("{id}/delete")]
        [HttpPost]
        public HttpResponseMessage Delete(int id)
        {
            _userRepository.DeleteRange(p => p.Id == id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("{id}/reset-password/update")]
        [HttpPost]
        public async Task<HttpResponseMessage> ResetPassword(int id, ResetPassword model)
        {
            await UserManager.RemovePasswordAsync(id);
            await UserManager.AddPasswordAsync(id, model.Password);
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [Route("change-password/update")]
        [HttpPost]
        public HttpResponseMessage ChangePassword(ChangePassword model)
        {
            var result = UserManager.ChangePassword(_workContext.CurrentUserId, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, result.Errors);
            }
        }
    }
}
