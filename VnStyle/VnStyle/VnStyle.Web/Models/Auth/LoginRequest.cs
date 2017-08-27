using FluentValidation.Attributes;

namespace VnStyle.Web.Models.Auth
{

    [Validator(typeof(LoginRequestValidator))]
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}