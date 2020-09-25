using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Application.Wrappers;
using AspNetCoreHero.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Application.Features.Products.Queries.GetById
{
    public class GetProductByIdQuery : IRequest<ResponseBase<Product>>
    {
        public int Id { get; set; }
        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ResponseBase<Product>>
        {
            private readonly IProductRepositoryAsync _productRepository;
            public GetProductByIdQueryHandler(IProductRepositoryAsync productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<ResponseBase<Product>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(query.Id);
                return new ResponseBase<Product>(product);
            }
        }
    }
}
