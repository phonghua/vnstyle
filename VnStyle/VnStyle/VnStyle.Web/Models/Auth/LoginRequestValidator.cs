using FluentValidation;

namespace VnStyle.Web.Models.Auth
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        public LoginRequestValidator()
        {
            RuleFor(p => p.UserName).Must(p => !string.IsNullOrEmpty(p)).WithMessage("Vui lòng nhập email đăng nhập");
            RuleFor(p => p.Password).Must(p => !string.IsNullOrEmpty(p)).WithMessage("Vui lòng nhập mật khẩu");
        }
    }
}