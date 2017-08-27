using System;
using System.Collections.Generic;
using System.Linq;
using Ricky.Infrastructure.Core;
using Ricky.Infrastructure.Core.Caching;
using VnStyle.Services.Data;
using VnStyle.Services.Data.Domain.Memberships;

namespace VnStyle.Services.Business
{
    public class UserService : IUserService
    {
        #region "Fields and Properties"
        private readonly IBaseRepository<AspNetRegister> _registerRepository;
        private readonly IBaseRepository<AspNetRole> _roleRepository;
        private readonly IBaseRepository<AspNetUserClaim> _userClaimRepository;
        private readonly IBaseRepository<AspNetUser> _userRepository;
        private readonly IBaseRepository<AspNetUserLogin> _userLoginRepository;
        private readonly IBaseRepository<AspNetUserRole> _userRoleRepository;
        private readonly ICacheManager _cacheManager;
        //private readonly IWorkContext _workContext;
        private readonly IBaseRepository<AspNetClient> _clientRepository;
        private readonly IBaseRepository<AspNetRefreshToken> _refreshTokenRepository;
        private readonly int cacheTime = 20;
        private readonly bool cacheNull = true;
        #endregion

        #region "Constructors"
        public UserService(IBaseRepository<AspNetRegister> registerRepository, IBaseRepository<AspNetRole> roleRepository,
            IBaseRepository<AspNetUserClaim> userClaimRepository, IBaseRepository<AspNetUser> userRepository,
            IBaseRepository<AspNetUserLogin> userLoginRepository, IBaseRepository<AspNetUserRole> userRoleRepository,
            ICacheManager cacheManager,
            //IWorkContext workContext, 
            IBaseRepository<AspNetClient> clientRepository, IBaseRepository<AspNetRefreshToken> refreshTokenRepository)
        {
            _registerRepository = registerRepository;
            _roleRepository = roleRepository;
            _userClaimRepository = userClaimRepository;
            _userRepository = userRepository;
            _userLoginRepository = userLoginRepository;
            _userRoleRepository = userRoleRepository;
            _cacheManager = cacheManager;
            //_workContext = workContext;
            _clientRepository = clientRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }



        #endregion


        public AspNetUser GetUserById(int userId)
        {
            return _cacheManager.Get(string.Format(CachingKey.CommonMembershipUserServiceUserById, userId), cacheTime, () =>
            {
                var user = _userRepository.GetAll(p => p.Id == userId && !p.IsDeleted).FirstOrDefault();
                if (user != null)
                {
                    _cacheManager.Set(string.Format(CachingKey.CommonMembershipUserServiceUserByUserName, user.UserName), user, cacheTime);
                    _cacheManager.Set(string.Format(CachingKey.CommonMembershipUserServiceUserByEmail, user.Email), user, cacheTime);
                }
                return user;
            }, cacheNull);
        }

        public AspNetUser GetUserByName(string userName)
        {

            userName = CommonHelper.EnsureNotNull(userName);
            if (string.IsNullOrEmpty(userName)) return null;
            userName = userName.ToLower().Trim();

            return _cacheManager.Get(string.Format(CachingKey.CommonMembershipUserServiceUserByUserName, userName), cacheTime, () =>
                 {
                     var user = _userRepository.GetAll(p => p.UserName == userName && !p.IsDeleted).FirstOrDefault();
                     if (user != null)
                     {
                         _cacheManager.Set(string.Format(CachingKey.CommonMembershipUserServiceUserById, user.Id), user, cacheTime);
                         _cacheManager.Set(string.Format(CachingKey.CommonMembershipUserServiceUserByEmail, user.Email), user, cacheTime);
                     }
                     return user;
                 }, cacheNull);
        }

        public AspNetUser GetUserByEmail(string email)
        {
            email = CommonHelper.EnsureNotNull(email);
            if (string.IsNullOrEmpty(email)) return null;
            email = email.ToLower().Trim();

            return _cacheManager.Get(string.Format(CachingKey.CommonMembershipUserServiceUserByEmail, email), cacheTime,
                () =>
                {
                    var user = _userRepository.GetAll(p => p.Email == email && !p.IsDeleted).FirstOrDefault();
                    if (user != null)
                    {
                        _cacheManager.Set(string.Format(CachingKey.CommonMembershipUserServiceUserById, user.Id), user, cacheTime);
                        _cacheManager.Set(string.Format(CachingKey.CommonMembershipUserServiceUserByUserName, user.UserName), user, cacheTime);
                    }
                    return user;
                }, cacheNull);

        }

        public AspNetUser InsertUser(AspNetUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.CreatedDate = DateTimeHelper.GetCurrentDateTime();
            user.Email = user.Email.Trim().ToLower();
            user.UserName = user.UserName.Trim().ToLower();
            user.CreatedDate = DateTimeHelper.GetCurrentDateTime();
            user.ModifiedDate = DateTimeHelper.GetCurrentDateTime();
            
            _userRepository.Insert(user);
            _userRepository.SaveChanges();
            if (cacheNull)
            {
                _cacheManager.Remove(string.Format(CachingKey.CommonMembershipUserServiceUserById, user.Id));
                _cacheManager.Remove(string.Format(CachingKey.CommonMembershipUserServiceUserByUserName, user.UserName));
                _cacheManager.Remove(string.Format(CachingKey.CommonMembershipUserServiceUserByEmail, user.Email));
            }
            return user;
        }

        public AspNetUser UpdateUser(AspNetUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.ModifiedDate = DateTimeHelper.GetCurrentDateTime();

            _userRepository.Update(user);
            _userRepository.SaveChanges();
            _cacheManager.Remove(string.Format(CachingKey.CommonMembershipUserServiceUserById, user.Id));
            _cacheManager.Remove(string.Format(CachingKey.CommonMembershipUserServiceUserByUserName, user.UserName));
            _cacheManager.Remove(string.Format(CachingKey.CommonMembershipUserServiceUserByEmail, user.Email));
            return user;
        }

        public IList<AspNetUser> GetAllUsers()
        {
            var data = _userRepository.Table.Where(p => !p.IsDeleted).ToList();

            if (data.Any())
            {
                data.ForEach(user =>
                {
                    _cacheManager.Set(string.Format(CachingKey.CommonMembershipUserServiceUserById, user.Id), user, cacheTime);
                    _cacheManager.Set(string.Format(CachingKey.CommonMembershipUserServiceUserByUserName, user.UserName), user, cacheTime);
                    _cacheManager.Set(string.Format(CachingKey.CommonMembershipUserServiceUserByEmail, user.Email), user, cacheTime);
                });
            }

            return data;
        }

        public void DeleteUser(int userId, int? deletedBy)
        {
            #region "Mark as deleted then save to database"

            var user = new AspNetUser { Id = userId, IsDeleted = true };
            _userRepository.Update(user, new[] { nameof(user.IsDeleted) });
            _userRepository.SaveChanges();

            #endregion

            #region "Clear cache"

            if (_cacheManager.IsSet(string.Format(CachingKey.CommonMembershipUserServiceUserById, user.Id)))
            {
                var userInCache = _cacheManager.Get<AspNetUser>(string.Format(CachingKey.CommonMembershipUserServiceUserById, user.Id));
                if (userInCache != null)
                {
                    _cacheManager.Remove(string.Format(CachingKey.CommonMembershipUserServiceUserById, userInCache.Id));
                    _cacheManager.Remove(string.Format(CachingKey.CommonMembershipUserServiceUserByUserName, userInCache.UserName));
                    _cacheManager.Remove(string.Format(CachingKey.CommonMembershipUserServiceUserByEmail, userInCache.Email));
                }
            }

            #endregion

        }

        public AspNetRole GetRoleById(int roleId)
        {
            return _cacheManager.Get(string.Format(CachingKey.CommonMembershipUserServiceRoleById, roleId), cacheTime,
                () =>
                 {
                     var role = _roleRepository.GetAll(p => p.Id == roleId ).FirstOrDefault();
                     if (role != null)
                     {
                         _cacheManager.Set(string.Format(CachingKey.CommonMembershipUserServiceRoleByName, role.Name), role, cacheTime);
                     }
                     return role;
                 }, cacheNull);

        }

        public IList<AspNetRole> GetRoleById(int[] roleIds)
        {
            if (roleIds == null || !roleIds.Any()) return new List<AspNetRole>();
            var data = _roleRepository.Table.Where(p => roleIds.Contains(p.Id) ).ToList();

            if (data.Any())
            {
                data.ForEach(role =>
                {
                    _cacheManager.Set(string.Format(CachingKey.CommonMembershipUserServiceRoleById, role.Id), role, cacheTime);
                    _cacheManager.Set(string.Format(CachingKey.CommonMembershipUserServiceRoleByName, role.Name), role, cacheTime);
                });
            }

            return data;
        }

        public AspNetRole GetRoleByName(string roleName)
        {
            roleName = CommonHelper.EnsureNotNull(roleName);
            if (string.IsNullOrEmpty(roleName)) return null;
            roleName = roleName.Trim().ToLower();
            return _cacheManager.Get(string.Format(CachingKey.CommonMembershipUserServiceRoleByName, roleName),
                cacheTime,
                () =>
                {
                    var role = _roleRepository.Table.FirstOrDefault(p => p.Name == roleName);
                    if (role != null)
                    {
                        _cacheManager.Set(string.Format(CachingKey.CommonMembershipUserServiceRoleById, role.Id), role, cacheTime);
                    }
                    return role;
                }, cacheNull);

        }

        public AspNetRole InsertRole(AspNetRole role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));

            InstanceHelper.EnsureCubeDateInfo(role);
            _roleRepository.Insert(role);
            _roleRepository.SaveChanges();

            if (cacheNull)
            {
                _cacheManager.Remove(string.Format(CachingKey.CommonMembershipUserServiceRoleById, role.Id));
                _cacheManager.Remove(string.Format(CachingKey.CommonMembershipUserServiceRoleByName, role.Name));
            }
            return role;
        }

        public void AddUserToRole(AspNetUserRole userRole)
        {
            if (userRole == null) throw new ArgumentNullException(nameof(userRole));

            _userRoleRepository.Insert(userRole);
            _userRoleRepository.SaveChanges();

            if (cacheNull)
            {
                _cacheManager.Remove(string.Format(CachingKey.CommonMembershipUserServiceUserInRole, userRole.UserId, userRole.RoleId));
            }
        }

        public void RemoveUserFromRole(AspNetUserRole userRole)
        {
            if (userRole == null) throw new ArgumentNullException(nameof(userRole));
            _userRoleRepository.DeleteRange(p => p.RoleId == userRole.RoleId && p.UserId == userRole.UserId);

            _cacheManager.Remove(string.Format(CachingKey.CommonMembershipUserServiceUserInRole, userRole.UserId, userRole.RoleId));
        }

        public IList<AspNetUserRole> GetUserRolesByUserId(int userId)
        {
            var data = _userRoleRepository.GetAll(p => p.UserId == userId).ToList();

            if (data.Any())
            {
                data.ForEach(userRole =>
                {
                    _cacheManager.Set(string.Format(CachingKey.CommonMembershipUserServiceUserInRole, userRole.UserId, userRole.RoleId), userRole, cacheTime);
                });
            }

            return data;
        }

        public bool UserInRole(int userId, int roleId)
        {
            var userInRole = (_cacheManager.Get(string.Format(CachingKey.CommonMembershipUserServiceUserInRole, userId, roleId), cacheTime,
                () =>
                {
                    return _userRoleRepository.GetAll(p => p.UserId == userId && p.RoleId == roleId).FirstOrDefault();
                }, cacheNull));
            return userInRole != null;
        }

        public AspNetUserLogin InsertUserLogin(AspNetUserLogin userLogin)
        {
            if (userLogin == null) throw new ArgumentNullException(nameof(userLogin));

            _userLoginRepository.Insert(userLogin);
            _userLoginRepository.SaveChanges();

            _cacheManager.Remove(string.Format(CachingKey.CommonMembershipUserServiceUserLoginByUserId, userLogin.UserId));
            _cacheManager.Remove(string.Format(CachingKey.CommonMembershipUserServiceUserLoginByProviderKeyAndLoginProvider, userLogin.ProviderKey, userLogin.LoginProvider));

            return userLogin;
        }

        public void DeleteUserLogin(AspNetUserLogin userLogin)
        {
            if (userLogin == null) throw new ArgumentNullException(nameof(userLogin));

            var effectedRows = _userLoginRepository.DeleteRange(p => p.LoginProvider == userLogin.LoginProvider && p.ProviderKey == userLogin.ProviderKey);

            _cacheManager.Remove(string.Format(CachingKey.CommonMembershipUserServiceUserLoginByUserId, userLogin.UserId));
            _cacheManager.Remove(string.Format(CachingKey.CommonMembershipUserServiceUserLoginByProviderKeyAndLoginProvider, userLogin.ProviderKey, userLogin.LoginProvider));
        }

        public IList<AspNetUserLogin> GetUserLogins(int userId)
        {
            return _cacheManager.Get(string.Format(CachingKey.CommonMembershipUserServiceUserLoginByUserId, userId),
                cacheTime,
                () =>
                {
                    return _userLoginRepository.GetAll(p => p.UserId == userId).ToList();
                }, cacheNull);

        }

        public AspNetUserLogin GetUserLogin(AspNetUserLogin userLogin)
        {
            return _cacheManager.Get(
                string.Format(CachingKey.CommonMembershipUserServiceUserLoginByProviderKeyAndLoginProvider,
                    userLogin.ProviderKey, userLogin.LoginProvider), cacheTime,
                () =>
                {
                    return _userLoginRepository.GetAll(p => p.LoginProvider == userLogin.LoginProvider &&
                    p.ProviderKey == userLogin.ProviderKey).FirstOrDefault();
                }, cacheNull);
        }

        public int? GetUserIdByUserLogin(string loginProvider, string providerKey)
        {
            var userLogin = _cacheManager.Get(
                string.Format(CachingKey.CommonMembershipUserServiceUserLoginByProviderKeyAndLoginProvider,
                    providerKey, loginProvider), cacheTime,
                () =>
                {
                    return _userLoginRepository.GetAll(p => p.LoginProvider == loginProvider &&
                    p.ProviderKey == providerKey).FirstOrDefault();
                }, cacheNull);


            return userLogin?.UserId;

            //var userId = _userLoginRepository.GetAll(p => p.LoginProvider == loginProvider &&
            //        p.ProviderKey == providerKey && !p.IsDeleted).Select(p => p.UserId).FirstOrDefault();
            //return userId == 0 ? null : (int?)userId;
        }

        public AspNetUserClaim InsertUserClaim(AspNetUserClaim userClaim)
        {
            if (userClaim == null) throw new ArgumentNullException(nameof(userClaim));

            _userClaimRepository.Insert(userClaim);
            return userClaim;
        }

        public IList<AspNetUserClaim> GetUserClaimsByUserId(int userId)
        {
            return _userClaimRepository.Table.Where(p => p.UserId == userId).ToList();
        }

        public bool IsExistEmail(string email)
        {
            email = CommonHelper.EnsureNotNull(email);
            if (!string.IsNullOrEmpty(email)) return false;

            if (_cacheManager.IsSet(string.Format(CachingKey.CommonMembershipUserServiceUserByEmail, email)))
            {

                var user = _cacheManager.Get<AspNetUser>(string.Format(CachingKey.CommonMembershipUserServiceUserByEmail, email));
                return user != null;
            }
            return _userRepository.Table.Where(p => p.Email == email).Select(p => true).FirstOrDefault();
        }

        public void Dispose()
        {

        }


        #region "Beare Authentization"

        public AspNetClient FindClient(string clientId)
        {
            return _clientRepository.Table.FirstOrDefault(p => p.Id == clientId);
        }

        public bool AddRefreshToken(AspNetRefreshToken token)
        {
            var existingToken = _refreshTokenRepository.Table.FirstOrDefault(p => p.Subject == token.Subject && p.ClientId == token.ClientId);
            if (existingToken != null)
            {
                RemoveRefreshToken(existingToken);
            }
            _refreshTokenRepository.Insert(token);
            return _refreshTokenRepository.SaveChanges() > 0;
        }

        public bool RemoveRefreshToken(string refreshTokenId)
        {
            _refreshTokenRepository.DeleteRange(p => p.TokenId == refreshTokenId);
            _refreshTokenRepository.SaveChanges();
            return true;
        }

        public bool RemoveRefreshToken(AspNetRefreshToken refreshToken)
        {
            var effectedRows = _refreshTokenRepository.DeleteRange(p => p.Id == refreshToken.Id);
            return effectedRows > 0;
        }

        public AspNetRefreshToken FindRefreshToken(string refreshTokenId)
        {
            return _refreshTokenRepository.Table.FirstOrDefault(p => p.TokenId == refreshTokenId);
        }

        public List<AspNetRefreshToken> GetAllRefreshTokens()
        {
            return _refreshTokenRepository.Table.ToList();
        }
        #endregion

        #region "User information"

        public UserBaseInfo GetUserBaseInfo(long userId)
        {
            return _userRepository.Table.Where(p => p.Id == userId)
                .Select(p => new UserBaseInfo { UserId = p.Id, DisplayName = p.UserName, Avatar = "/contents/app-images/user-default-female.png" })
                .FirstOrDefault();
        }

        #endregion
    }
}
