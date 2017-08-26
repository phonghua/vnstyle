using System;

namespace VnStyle.Services.Data.Domain.Memberships
{
    public class AspNetRegister
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime RegisteredDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int SignupTimes { get; set; }
    }
}