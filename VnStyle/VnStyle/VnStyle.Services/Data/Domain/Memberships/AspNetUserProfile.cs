using System;
using VnStyle.Services.Data.Enum;

namespace VnStyle.Services.Data.Domain.Memberships
{
    public class AspNetUserProfile 
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Skype { get; set; }
        public string Facebook { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string IdentityPassport { get; set; }
        public long? Nationality { get; set; }
        public EMaritalStatus? MaritalStatus { get; set; }
    }
}