using Microsoft.AspNet.Identity;
using VnStyle.Services.Data.Domain.Memberships;

namespace VnStyle.Web.Infrastructure.Security
{
    public class IdentityRole : AspNetRole, IRole<int>
    {
        public IdentityRole()
        {
            //Id = Guid.NewGuid().ToString().ToLower();
        }

        public IdentityRole(string roleName)
            : this()
        {
            Name = roleName;
        }
        public IdentityRole(int id, string roleName)
        {
            Id = id;
            Name = roleName;
        }

        
    }
}
