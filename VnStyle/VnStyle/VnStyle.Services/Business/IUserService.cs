using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VnStyle.Services.Business
{
    public interface IUserService
    {
    
        //#region "Users"

        //AspNetUser GetUserById(long userId);

        //AspNetUser GetUserByName(string userName);

        //AspNetUser GetUserByEmail(string email);

        //AspNetUser InsertUser(AspNetUser user);

        //AspNetUser UpdateUser(AspNetUser user);

        //IList<AspNetUser> GetAllUsers();

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="userId"></param>
        ///// <param name="deletedBy">current user logged in if null</param>
        //void DeleteUser(long userId, long? deletedBy);


        //#endregion

        //#region "Roles"

        //AspNetRole GetRoleById(long roleId);
        //IList<AspNetRole> GetRoleById(long[] roleIds);
        //AspNetRole GetRoleByName(string roleName);
        //AspNetRole InsertRole(AspNetRole role);

        //#endregion

        //#region "User roles"

        //void AddUserToRole(AspNetUserRole userRole);

        //void RemoveUserFromRole(AspNetUserRole userRole);

        //IList<AspNetUserRole> GetUserRolesByUserId(long userId);

        //bool UserInRole(long userId, long roleId);

        //#endregion

        //#region "User login"

        //AspNetUserLogin InsertUserLogin(AspNetUserLogin userLogin);

        //void DeleteUserLogin(AspNetUserLogin userLogin);

        //IList<AspNetUserLogin> GetUserLogins(long userId);

        //AspNetUserLogin GetUserLogin(AspNetUserLogin userLogin);

        //long? GetUserIdByUserLogin(string loginProvider, string providerKey);

        //#endregion

        //#region "User claims"

        //AspNetUserClaim InsertUserClaim(AspNetUserClaim userClaim);

        //IList<AspNetUserClaim> GetUserClaimsByUserId(long userId);




        //#endregion

        //#region "Web signup"

        ////SignupResult Signup(SignupModel model);

        //#endregion

        //#region "Beare Authentization"

        //AspNetClient FindClient(string clientId);
        //bool AddRefreshToken(AspNetRefreshToken token);
        //bool RemoveRefreshToken(string refreshTokenId);
        //bool RemoveRefreshToken(AspNetRefreshToken refreshToken);
        //AspNetRefreshToken FindRefreshToken(string refreshTokenId);
        //List<AspNetRefreshToken> GetAllRefreshTokens();
        //#endregion

        //#region "Ext"

        ////Dtos.BaseUserInfoModel ToBaseUserInfo(AspNetUser aspNetUser);
        ////Dtos.BaseUserInfoModel ToBaseUserInfo(int userId);
        //bool IsExistEmail(string email);


        //#endregion

        //#region "User information"
        //UserBaseInfo GetUserBaseInfo(long userId);

        //#endregion

    }
}
