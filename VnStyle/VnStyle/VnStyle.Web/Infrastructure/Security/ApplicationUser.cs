using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace VnStyle.Web.Infrastructure.Security
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager, string applicationType = DefaultAuthenticationTypes.ApplicationCookie)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, applicationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    //public class ApplicationDbContext : IDisposable
    //{
    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }

    //    public void Dispose()
    //    {

    //    }
    //}
}