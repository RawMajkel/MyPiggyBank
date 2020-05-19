using FluentValidation;
using MyPiggyBank.Core.Communication.Account.Requests;

namespace MyPiggyBank.Core.Communication.Account.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(l => l.Password)
                .NotEmpty();

            RuleFor(l => l.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
