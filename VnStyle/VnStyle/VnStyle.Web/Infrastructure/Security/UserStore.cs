using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Ricky.Infrastructure.Core;
using Ricky.Infrastructure.Core.ObjectContainer;
using ServiceStack.Common;
using VnStyle.Services.Business;
using VnStyle.Services.Data.Domain.Memberships;

namespace VnStyle.Web.Infrastructure.Security
{
    public class UserStore<TUser> : IUserLoginStore<TUser, int>,
        IUserClaimStore<TUser, int>,
        IUserRoleStore<TUser, int>,
        IUserPasswordStore<TUser, int>,
        IUserSecurityStampStore<TUser, int>,
        IQueryableUserStore<TUser, int>,
        IUserEmailStore<TUser, int>,
        IUserPhoneNumberStore<TUser, int>,
        IUserTwoFactorStore<TUser, int>,
        IUserLockoutStore<TUser, int>,
        IUserStore<TUser, int> where TUser : IdentityUser, new()
    {
        #region "Fields and Properties"
        private readonly IUserService _userService;
        private readonly IWorkContext _workContext;
        #endregion

        #region "Constructors"
        public UserStore(IUserService userService)
        {
            _userService = userService;
            _workContext = EngineContext.Current.Resolve<IWorkContext>();
        }

        #endregion

        public void Dispose()
        {

        }

        public Task CreateAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException("user");

            _userService.InsertUser(user.TranslateTo<AspNetUser>());
            return Task.FromResult<object>(null);
        }

        public Task UpdateAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException("user");
            var userEntity = _userService.GetUserById(user.Id);
            if (userEntity != null)
            {
                //userEntity.Email = user.Email;
                //userEntity.EmailConfirmed = user.EmailConfirmed;
                //userEntity.PasswordHash = user.PasswordHash;
                //userEntity.SecurityStamp = user.SecurityStamp;
                //userEntity.PhoneNumber = user.PhoneNumber;
                //userEntity.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                //userEntity.TwoFactorEnabled = user.TwoFactorEnabled;
                //userEntity.LockoutEndDate = user.LockoutEndDate;
                //userEntity.LockoutEnabled = user.LockoutEnabled;
                //userEntity.AccessFailedCount = user.AccessFailedCount;

                //_userService.UpdateUser(userEntity);
            }

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException("user");

            int? deletedBy = null;
            if (_workContext.IsAuthenticated) deletedBy = _workContext.CurrentUserId;
            _userService.DeleteUser(user.Id, deletedBy);
            return Task.FromResult<object>(null);
        }

        public Task<TUser> FindByIdAsync(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user != null) return Task.FromResult(user.TranslateTo<TUser>());
            return Task.FromResult<TUser>(null);
        }

        //public Task<TUser> FindByIdAsync(string userId)
        //{
        //    if (string.IsNullOrEmpty(userId))
        //    {
        //        throw new ArgumentException("Null or empty argument: userId");
        //    }

        //}

        public Task<TUser> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Null or empty argument: userName");
            }
            var user = _userService.GetUserByName(userName);
            if (user != null)
                return Task.FromResult(user.TranslateTo<TUser>());
            else return Task.FromResult<TUser>(null);
        }

        public Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (login == null) throw new ArgumentNullException("login");

            _userService.InsertUserLogin(new AspNetUserLogin() { LoginProvider = login.LoginProvider, ProviderKey = login.ProviderKey, UserId = user.Id });
            return Task.FromResult<object>(null);
        }

        public Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (login == null) throw new ArgumentNullException("login");
            _userService.DeleteUserLogin(new AspNetUserLogin() { LoginProvider = login.LoginProvider, ProviderKey = login.ProviderKey, UserId = user.Id });
            return Task.FromResult<object>(null);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException("user");

            var result = _userService.GetUserLogins(user.Id);
            if (result != null && result.Any())
                return Task.FromResult<IList<UserLoginInfo>>(result.Select(p => new UserLoginInfo(p.LoginProvider, p.ProviderKey)).ToList());

            return Task.FromResult<IList<UserLoginInfo>>(new List<UserLoginInfo>());
        }

        public Task<TUser> FindAsync(UserLoginInfo login)
        {
            if (login == null) throw new ArgumentNullException("login");

            var userId = _userService.GetUserIdByUserLogin(login.LoginProvider, login.ProviderKey);
            if (!userId.HasValue) return Task.FromResult<TUser>(null);
            var user = _userService.GetUserById(userId.Value);
            if (user != null) return Task.FromResult(user.TranslateTo<TUser>());
            return Task.FromResult<TUser>(null);
        }

        public Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException("user");
            var claims = _userService.GetUserClaimsByUserId(user.Id);

            var result = claims.Select(p => new Claim(p.ClaimType, p.ClaimValue)).ToList();

            return Task.FromResult<IList<Claim>>(result);
        }

        public Task AddClaimAsync(TUser user, Claim claim)
        {
            _userService.InsertUserClaim(new AspNetUserClaim()
            {
                UserId = user.Id,
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
            });
            return Task.FromResult<object>(null);
        }

        public Task RemoveClaimAsync(TUser user, Claim claim)
        {

            throw new NotImplementedException();
        }

        public Task AddToRoleAsync(TUser user, string roleName)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (string.IsNullOrEmpty(roleName)) throw new ArgumentNullException("roleName");

            var role = _userService.GetRoleByName(roleName);
            if (role == null) throw new ArgumentException(String.Format("roleName: '{0}' not found", roleName));

            var userRole = new AspNetUserRole() { UserId = user.Id, RoleId = role.Id };
            _userService.AddUserToRole(userRole);
            return Task.FromResult<object>(null);
        }

        public Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (string.IsNullOrEmpty(roleName)) throw new ArgumentNullException("roleName");

            var role = _userService.GetRoleByName(roleName);
            if (role == null) throw new ArgumentException(String.Format("roleName: '{0}' not found", roleName));

            _userService.RemoveUserFromRole(new AspNetUserRole() { UserId = user.Id, RoleId = role.Id });

            return Task.FromResult<object>(null);
        }

        public Task<IList<string>> GetRolesAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException("user");

            var userRoles = _userService.GetUserRolesByUserId(user.Id);
            var roleIds = userRoles.Select(p => p.RoleId).ToArray();
            return Task.FromResult<IList<string>>(_userService.GetRoleById(roleIds).Select(p => p.Name).ToList());

        }

        public Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            if (user == null) throw new ArgumentNullException("user");

            var role = _userService.GetRoleByName(roleName);
            if (role == null) throw new Exception("role not found");

            return Task.FromResult(_userService.UserInRole(user.Id, role.Id));
        }

        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            if (user == null) throw new ArgumentNullException("user");

            if (user.Id > 0)
            {
                var userEntity = _userService.GetUserById(user.Id);
                userEntity.PasswordHash = passwordHash;
                _userService.UpdateUser(userEntity);
            }
            return Task.FromResult<object>(null);
        }

        public Task<string> GetPasswordHashAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            if (user == null) throw new ArgumentNullException("user");
            //user.SecurityStamp = stamp;
            if (user.Id > 0)
            {
                var userEntity = _userService.GetUserById(user.Id);
                userEntity.SecurityStamp = stamp;
                _userService.UpdateUser(userEntity);
            }
            return Task.FromResult<object>(null);
        }

        public Task<string> GetSecurityStampAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.FromResult(user.SecurityStamp);
        }

        public IQueryable<TUser> Users
        {
            get
            {
                var users = _userService.GetAllUsers();
                if (users == null || users.Count == 0) return Enumerable.Empty<TUser>().AsQueryable();
                return users.Select(aspNetUser => aspNetUser as TUser).AsQueryable();
            }
        }

        public Task SetEmailAsync(TUser user, string email)
        {
            if (user == null) throw new ArgumentNullException("user");
            user.Email = email;
            if (user.Id > 0)
            {
                _userService.UpdateUser(user);
            }
            return Task.FromResult<object>(null);
        }

        public Task<string> GetEmailAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            if (user == null) throw new ArgumentNullException("user");
            user.EmailConfirmed = confirmed;
            if (user.Id > 0)
            {
                _userService.UpdateUser(user);
            }
            return Task.FromResult<object>(null);
        }

        public Task<TUser> FindByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Null or empty argument: email");
            }
            var user = _userService.GetUserByEmail(email) as TUser;
            if (user != null) return Task.FromResult(user);
            else return Task.FromResult<TUser>(null);
        }

        public Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            if (user == null) throw new ArgumentNullException("user");
            user.PhoneNumber = phoneNumber;
            if (user.Id > 0)
            {
                _userService.UpdateUser(user);
            }
            return Task.FromResult<object>(null);
        }

        public Task<string> GetPhoneNumberAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            if (user == null) throw new ArgumentNullException("user");
            user.PhoneNumberConfirmed = confirmed;
            if (user.Id > 0)
            {
                _userService.UpdateUser(user);
            }
            return Task.FromResult<object>(null);
        }

        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            if (user == null) throw new ArgumentNullException("user");
            user.TwoFactorEnabled = enabled;
            if (user.Id > 0) _userService.UpdateUser(user);
            return Task.FromResult<object>(null);
        }

        public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException("user");

            return Task.FromResult(DateTimeOffset.UtcNow.AddDays(-1));

            //TimeZone.CurrentTimeZone.ToLocalTime(user.LockoutEndDateUtc.Value);
            //var ts = DateTime.UtcNow - DateTime.Now;
            //return Task.FromResult(ts);
        }

        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            if (user == null) throw new ArgumentNullException("user");
            user.LockoutEndDate = lockoutEnd.UtcDateTime;
            if (user.Id > 0)
            {
                _userService.UpdateUser(user);
            }
            return Task.FromResult<object>(null);
        }

        public Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException("user");
            user.AccessFailedCount++;
            if (user.Id > 0) _userService.UpdateUser(user);
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task ResetAccessFailedCountAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException("user");

            user.AccessFailedCount = 0;
            if (user.Id > 0) _userService.UpdateUser(user);
            return Task.FromResult<object>(null);
        }

        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException("user");
            return Task.FromResult(user.LockoutEnabled);
        }

        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            if (user == null) throw new ArgumentNullException("user");
            user.LockoutEnabled = enabled;
            if (user.Id > 0)
            {
                _userService.UpdateUser(user);
            }
            return Task.FromResult<object>(null);
        }
    }
}
