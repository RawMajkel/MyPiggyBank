using FluentValidation;

namespace MyPiggyBank.Core.Communication.Account.Requests
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequest> {
        public LoginRequestValidator() {
            RuleFor(l => l.Password)
                .NotEmpty().WithMessage(AccountResources.LoginRequestValidator_Password_Empty_Error);

            RuleFor(l => l.Email)
                .NotEmpty().WithMessage(AccountResources.LoginRequestValidator_Email_Empty_Error)
                .EmailAddress().WithMessage(AccountResources.LoginRequestValidator_Email_NotValid_Error);
        }
    }
}
