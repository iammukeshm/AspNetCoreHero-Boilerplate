using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreHero.Application.Features.Products.Commands.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {

        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Barcode)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
                .WithMessage("{PropertyName} already exists.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MinimumLength(10)
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        }
    }
}
