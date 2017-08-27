using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VnStyle.Services.Data.Domain.Memberships;

namespace VnStyle.Services.Business
{
    public interface IUserService
    {

        #region "Users"

        AspNetUser GetUserById(int userId);

        AspNetUser GetUserByName(string userName);

        AspNetUser GetUserByEmail(string email);

        AspNetUser InsertUser(AspNetUser user);

        AspNetUser UpdateUser(AspNetUser user);

        IList<AspNetUser> GetAllUsers();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="deletedBy">current user logged in if null</param>
        void DeleteUser(int userId, int? deletedBy);


        #endregion

        #region "Roles"

        AspNetRole GetRoleById(int roleId);
        IList<AspNetRole> GetRoleById(int[] roleIds);
        AspNetRole GetRoleByName(string roleName);
        AspNetRole InsertRole(AspNetRole role);

        #endregion

        #region "User roles"

        void AddUserToRole(AspNetUserRole userRole);

        void RemoveUserFromRole(AspNetUserRole userRole);

        IList<AspNetUserRole> GetUserRolesByUserId(int userId);

        bool UserInRole(int userId, int roleId);

        #endregion

        #region "User login"

        AspNetUserLogin InsertUserLogin(AspNetUserLogin userLogin);

        void DeleteUserLogin(AspNetUserLogin userLogin);

        IList<AspNetUserLogin> GetUserLogins(int userId);

        AspNetUserLogin GetUserLogin(AspNetUserLogin userLogin);

        int? GetUserIdByUserLogin(string loginProvider, string providerKey);

        #endregion

        #region "User claims"

        AspNetUserClaim InsertUserClaim(AspNetUserClaim userClaim);

        IList<AspNetUserClaim> GetUserClaimsByUserId(int userId);




        #endregion

        #region "Web signup"

        //SignupResult Signup(SignupModel model);

        #endregion

        #region "Beare Authentization"

        AspNetClient FindClient(string clientId);
        bool AddRefreshToken(AspNetRefreshToken token);
        bool RemoveRefreshToken(string refreshTokenId);
        bool RemoveRefreshToken(AspNetRefreshToken refreshToken);
        AspNetRefreshToken FindRefreshToken(string refreshTokenId);
        List<AspNetRefreshToken> GetAllRefreshTokens();
        #endregion

        #region "Ext"

        //Dtos.BaseUserInfoModel ToBaseUserInfo(AspNetUser aspNetUser);
        //Dtos.BaseUserInfoModel ToBaseUserInfo(int userId);
        bool IsExistEmail(string email);


        #endregion

        //#region "User information"
        //UserBaseInfo GetUserBaseInfo(long userId);

        //#endregion

    }
}
