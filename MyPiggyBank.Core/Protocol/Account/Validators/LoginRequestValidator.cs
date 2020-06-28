using FluentValidation;
using MyPiggyBank.Core.Protocol.Account.Requests;

namespace MyPiggyBank.Core.Protocol.Account.Validators
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
