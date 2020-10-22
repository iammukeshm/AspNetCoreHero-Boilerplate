using AspNetCoreHero.Application.Exceptions;
using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Application.Wrappers;
using AspNetCoreHero.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Application.Features.ProductCategories.Queries.GetById
{
    public class GetProductCategoryByIdQuery : IRequest<Response<ProductCategory>>
    {
        public int Id { get; set; }
        public class GetProductByIdQueryHandler : IRequestHandler<GetProductCategoryByIdQuery, Response<ProductCategory>>
        {
            private readonly IProductCategoryRepositoryAsync _productCategoryRepository;
            public GetProductByIdQueryHandler(IProductCategoryRepositoryAsync productCategoryRepository)
            {
                _productCategoryRepository = productCategoryRepository;
            }
            public async Task<Response<ProductCategory>> Handle(GetProductCategoryByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await _productCategoryRepository.GetByIdAsync(query.Id);
                if (product == null) throw new NotFoundException(nameof(ProductCategory),query.Id);
                return new Response<ProductCategory>(product);
            }
        }
    }
}
