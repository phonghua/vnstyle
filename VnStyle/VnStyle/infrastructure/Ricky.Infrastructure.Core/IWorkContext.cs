using System;

namespace Ricky.Infrastructure.Core
{
    public interface IWorkContext
    {
        bool IsAuthenticated { get; }
        int CurrentUserId { get; }

        int? CurrentCompanyId { get; }
        int? CurrentMarkupId { get; }


        //MerchantInfo Merchant { get; }
        //StoreInfo WorkingStore { get; }
        //bool IsMerchantLogin { get; }
        //void SetWorkingStore(int storeId);
        //MerchantInfo LoadWorkingMerchant();
        bool IsAuthorized(string permissionName);
        bool IsAuthorized(int userId, string permissionName);
        int Gmt { get; }
        UserBaseInfo GetUserBaseInfo(int userId);
        string CurrentLanguage { get; }
    }

    public class MerchantInfo
    {
        public int MerchantId { get; set; }
        public string MerchantName { get; set; }
        public DateTime ExpiredDate { get; set; }
        public bool IsExpired { get; set; }
    }

    public class StoreInfo
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
    }

    public class UserBaseInfo
    {
        public int UserId { get; set; }
        public string DisplayName { get; set; }
        //public DateTime? Birthday { get; set; }
        public string Avatar { get; set; }
    }
}
