using FluentValidation;
using GenericApp.Domain.Dto.Models;
using GenericApp.Infra.CC.Localization.Resources;
using System;

namespace GenericApp.Application.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(c => c)
                .NotNull()
                .WithMessage(x =>
                {
                    throw new ArgumentException(SharedResource.ObjectMustNotBeNull, SharedResource.User);
                });

            RuleFor(c => c.Email)
               .NotEmpty().NotNull()
               .WithMessage(string.Format(SharedResource.FieldMustBeInformed, SharedResource.Email));

            RuleFor(c => c.Email)
                .EmailAddress()
                .WithMessage(SharedResource.MustBeValidMailAddress);
        }
    }
}
