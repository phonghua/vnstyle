namespace VnStyle.Services.Data.Domain.Memberships
{
    public class AspNetUserClaim 
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}