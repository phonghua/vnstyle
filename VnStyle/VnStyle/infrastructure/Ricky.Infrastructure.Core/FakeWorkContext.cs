namespace Ricky.Infrastructure.Core
{
    public class FakeWorkContext : IWorkContext
    {


        public bool IsAuthenticated { get { return true; } }
        public int CurrentUserId { get { return 1; } }
        public int? CurrentCompanyId { get { return 1; } }
        public int? CurrentMarkupId { get { return 5; } }
        public bool IsAuthorized(string permissionName) { return true; }
        public bool IsAuthorized(int userId, string permissionName) { return true; }
        public int Gmt { get { return 7; } }
        public UserBaseInfo GetUserBaseInfo(int userId)
        {
            return new UserBaseInfo
            {
                UserId = userId,
                Avatar = "https://lh3.googleusercontent.com/-eOISP5914ic/AAAAAAAAAAI/AAAAAAAAAAA/ADPlhfKd1zTOT_APQP3JVYkLCpAfkd3Zig/s96-c-mo/photo.jpg",
                DisplayName = "Hua Dai Phong"
            };
        }

        public string CurrentLanguage => "vi";
    }
}