using FluentValidation;
using GenericApp.Domain.Dto.Request;
using GenericApp.Infra.CC.Localization.Resources;
using System;
using System.Text.RegularExpressions;

namespace GenericApp.Application.Validators
{
    public class LoginValidator : AbstractValidator<CredentialsDto>
    {
        public LoginValidator()
        {
            RuleFor(c => c)
                .NotNull()
                .WithMessage(x =>
                {
                    throw new ArgumentException(SharedResource.ObjectMustNotBeNull, SharedResource.Login);
                });

            RuleFor(c => c.Username)
               .NotEmpty().NotNull()
               .WithMessage(string.Format(SharedResource.FieldMustBeInformed, SharedResource.Email));

            RuleFor(c => c.Username)
                .EmailAddress()
                .WithMessage(SharedResource.MustBeValidMailAddress);

            RuleFor(c => c.Password)
                .NotEmpty().NotNull()
                .WithMessage(string.Format(SharedResource.FieldMustBeInformed, SharedResource.Password));

            RuleFor(c => c.Password.Length)
                .GreaterThan(5).LessThan(11)
                .WithMessage(SharedResource.PasswordMustBeBetween6And10);

            RuleFor(x => x.Password).Custom((password, context) =>
            {
                bool containsNumber = Regex.IsMatch(password, @"\d");
                bool containsletters = Regex.IsMatch(password, "[a-zA-Z]");
                if (!containsNumber || !containsletters)
                {
                    context.AddFailure(SharedResource.PasswordMustContainLettersAndNumbers);
                }
            });
        }
    }
}
