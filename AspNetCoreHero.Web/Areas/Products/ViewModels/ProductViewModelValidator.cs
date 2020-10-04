using AspNetCoreHero.Application.Features.Products.Queries.UniqueBarcode;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Areas.Products.ViewModels
{
    public class ProductViewModelValidator : AbstractValidator<ProductViewModel>
    {
        private IMediator _mediator;

        public ProductViewModelValidator(IMediator mediator)
        {
            _mediator = mediator;
            RuleFor(p => p.Barcode)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        }
    }
}
