using AutoMapper;
using FluentValidation;
using MyPiggyBank.Data;
using MyPiggyBank.Data.Model;
using System;
using System.Text;

namespace MyPiggyBank.Core.Protocol
{
    public class LoginRequest {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class RegisterRequest {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateResult {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }

    public class LoginResponse {
        public string Identifier { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public LoginResponse(string identifier, string token, DateTime expiration)
        {
            Identifier = identifier;
            Token = token;
            Expiration = expiration;
        }

        public override string ToString()
        {
            var builder = new StringBuilder()
                .AppendLine($"Identifier: {Identifier}")
                .AppendLine($"Token: {Token}")
                .AppendLine($"Expires at: {Expiration.ToString("dd MMM yyyy HH:mm:ss")}");

            return builder.ToString();
        }
    }

    public class AccountsProfile : Profile
    {
        public AccountsProfile()
        {
            CreateMap<RegisterRequest, User>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.PasswordHash, opt => opt.Ignore())
                .ForMember(d => d.OperationCategories, opt => opt.Ignore())
                .ForMember(d => d.Resources, opt => opt.Ignore());

            CreateMap<User, AuthenticateResult>();
        }
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequest> {
        public LoginRequestValidator() {
            RuleFor(l => l.Password)
                .NotEmpty().WithMessage(ValidationResources.LoginRequestValidator_Password_Empty_Error);

            RuleFor(l => l.Email)
                .NotEmpty().WithMessage(ValidationResources.LoginRequestValidator_Email_Empty_Error)
                .EmailAddress().WithMessage(ValidationResources.LoginRequestValidator_Email_NotValid_Error);
        }
    }

    public class RegisterRequestValidator : AbstractValidator<RegisterRequest> {
        public RegisterRequestValidator() {
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage(ValidationResources.RegisterRequestValidator_Email_Empty_Error)
                .EmailAddress().WithMessage(ValidationResources.RegisterRequestValidator_Email_NotValid_Error);

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage(ValidationResources.RegisterRequestValidator_Password_Empty_Error)
                .MinimumLength(8).WithMessage(ValidationResources.RegisterRequestValidator_Password_Length_Error)
                .Matches("[A-Z]").WithMessage(ValidationResources.RegisterRequestValidator_Password_UpperCaseLetter_Error)
                .Matches("[0-9]").WithMessage(ValidationResources.RegisterRequestValidator_Password_Digit_Error)
                .Matches("[^a-zA-z0-9]").WithMessage(ValidationResources.RegisterRequestValidator_Password_SpecialCharacter_Error);
        }
    }
}
