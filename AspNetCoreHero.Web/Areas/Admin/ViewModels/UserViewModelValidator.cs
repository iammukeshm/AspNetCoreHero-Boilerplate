using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Areas.Admin.ViewModels
{
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        private IMediator _mediator;

        public UserViewModelValidator(IMediator mediator)
        {
            _mediator = mediator;
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Email)
                .EmailAddress().WithMessage("Enter a valid Email Id.")
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MinimumLength(6).WithMessage("{PropertyName} should have atleast 6 characters.")
                .NotNull();

            RuleFor(p => p.ConfirmPassword)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Equal(p => p.Password).WithMessage("Passwords do not match.");

        }
    }
}
