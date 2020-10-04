using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Application.Features.Products.Queries.UniqueBarcode
{
    public class IsUniqueBarcodeQuery : IRequest<bool>
    {
        public string barcode { get; set; }
        public class IsUniqueBarcodeQueryHandler : IRequestHandler<IsUniqueBarcodeQuery, bool>
        {
            private readonly IProductRepositoryAsync _productRepository;
            public IsUniqueBarcodeQueryHandler(IProductRepositoryAsync productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<bool> Handle(IsUniqueBarcodeQuery query, CancellationToken cancellationToken)
            {
                var result = await _productRepository.IsUniqueBarcodeAsync(query.barcode);
                return result;
            }
        }
    }
}
