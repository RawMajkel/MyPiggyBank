using FluentValidation;
using MyPiggyBank.Core.Protocol.Account;
using MyPiggyBank.Core.Protocol.Account.Requests;

namespace MyPiggyBank.Core.Protocol.Account.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage(AccountResources.RegisterRequestValidator_Email_Empty_Error)
                .EmailAddress()
                .WithMessage(AccountResources.RegisterRequestValidator_Email_NotValid_Error);

            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage(AccountResources.RegisterRequestValidator_Password_Empty_Error)
                .MinimumLength(8)
                .WithMessage(AccountResources.RegisterRequestValidator_Password_Length_Error)
                .Matches("[A-Z]")
                .WithMessage(AccountResources.RegisterRequestValidator_Password_UpperCaseLetter_Error)
                .Matches("[0-9]")
                .WithMessage(AccountResources.RegisterRequestValidator_Password_Digit_Error)
                .Matches("[^a-zA-z0-9]")
                .WithMessage(AccountResources.RegisterRequestValidator_Password_SpecialCharacter_Error);
        }
    }
}
