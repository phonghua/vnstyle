namespace VnStyle.Services.Business
{
    public static class CachingKey
    {
        #region "User service"
        public static int CommonMembershipUserServiceCacheTime = 20;
        public static string CommonMembershipUserServicePattern = "CachingKey.CommonMembershipUserService";
        public static string CommonMembershipUserServiceUserById = CommonMembershipUserServicePattern + ".UserById.{0}";
        public static string CommonMembershipUserServiceUserByUserName = CommonMembershipUserServicePattern + ".UserByUserName.{0}";
        public static string CommonMembershipUserServiceUserByEmail = CommonMembershipUserServicePattern + ".UserByEmail.{0}";

        public static string CommonMembershipUserServiceRoleById = CommonMembershipUserServicePattern + ".RoleById.{0}";
        public static string CommonMembershipUserServiceRoleByName = CommonMembershipUserServicePattern + ".RoleByName.{0}";

        public static string CommonMembershipUserServiceUserInRole = CommonMembershipUserServicePattern + ".UserId.{0}.RoleId.{1}";

        public static string CommonMembershipUserServiceUserLoginByUserId = CommonMembershipUserServicePattern + ".UserLogin.ByUserId.{0}";
        public static string CommonMembershipUserServiceUserLoginByProviderKeyAndLoginProvider = CommonMembershipUserServicePattern + ".UserLogin.ProviderKey.{0}.LoginProvider.{1}";

        #endregion

        #region "Category service"

        public static int CategoryServiceCacheTime = 20;
        public static string CategoryServicePattern = "CachingKey.CategoryService";
        public static string CategoryServiceAvailableCategory = "CachingKey.CategoryService.Available";
        #endregion

        #region "File service"

        public static int FileServiceCacheTime = 20;
        public static string FileServicePattern = "CachingKey.FileService";
        public static string FileServiceById = "CachingKey.FileService.FileId.{0}";

        #endregion

        #region "Setting service"

        public static string SettingServicePattern = "CachingKey.SettingService";

        #endregion

        #region "Markup service"
        public static string MarkupServicePattern = "CachingKey.MarkupService";
        public static int MarkupServiceCacheTime = 20;
        public static string MarkupServiceById = MarkupServicePattern + ".MarkupId.{0}";
        public static string MarkupServiceGallery = MarkupServiceById + ".Gallery.G_Type{1}";
        public static string MarkupServiceServiceItem = MarkupServiceById + ".ServiceItem";
        #endregion

        #region "Service item"
        public static string ServiceItemServicePattern = "CachingKey.MarkupService";
        public static int ServiceItemServiceCacheTime = 20;
        public static string ServiceItemServiceGetAllAlsoLoadDeletedItems = ServiceItemServicePattern + ".AlsoLoadDeletedItems.{0}";
        #endregion

        #region "Location service"
        #endregion

    }
}
