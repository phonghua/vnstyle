using Microsoft.AspNet.Identity;
using VnStyle.Services.Data.Domain.Memberships;

namespace VnStyle.Web.Infrastructure.Security
{
    #region "Default Identity user"
    public class IdentityUser : AspNetUser, IUser<int>
    {
        public IdentityUser()
        {
            //Id = Guid.NewGuid().ToString().ToLower();
        }

        public IdentityUser(string userName)
            : this()
        {
            UserName = userName;
        }
    }

    #endregion


}
