using System;

namespace VnStyle.Services.Data.Domain.Memberships
{
    public class AspNetUser 
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDate { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        //public int UserType { get; set; }
        public long UserProfileId { get; set; }
        public bool IsExternal { get; set; }

        
    }
}
