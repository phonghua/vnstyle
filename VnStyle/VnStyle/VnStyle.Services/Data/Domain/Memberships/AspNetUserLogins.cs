namespace VnStyle.Services.Data.Domain.Memberships
{
    public class AspNetUserLogin 
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public int UserId { get; set; }
    }
}