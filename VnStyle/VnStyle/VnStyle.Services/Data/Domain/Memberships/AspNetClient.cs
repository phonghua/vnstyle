using VnStyle.Services.Data.Enum;

namespace VnStyle.Services.Data.Domain.Memberships
{
    public class AspNetClient 
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }
        public EAspNetApplicationTypes ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        public string AllowedOrigin { get; set; }
    }
}