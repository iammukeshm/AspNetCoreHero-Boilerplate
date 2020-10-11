using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreHero.Application.Features.ProductCategories.Queries.GetAll;
using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Application.Wrappers;
using AspNetCoreHero.Domain.Entities;
using AutoMapper;
using MediatR;

namespace AspNetCoreHero.Application.Features.Products.Queries.GetAll
{

    public class GetAllProductsQuery : IRequest<Response<IEnumerable<GetAllProductsViewModel>>>
    {
        public bool ReturnImages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Response<IEnumerable<GetAllProductsViewModel>>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IProductRepositoryAsync productRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetAllProductsViewModel>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllProductsParameter>(request);
            var product = new List<Product>();
            if(request.ReturnImages)
            {
                var productWithImages = await _productRepository.GetAllWithCategoriesAsync();
                product = productWithImages.ToList();
            }
            else
            {
                var productWithoutImages = await _productRepository.GetAllWithCategoriesWithoutImagesAsync();
                product = productWithoutImages.ToList();
            }
            var productViewModel = _mapper.Map<IEnumerable<GetAllProductsViewModel>>(product);
            return new Response<IEnumerable<GetAllProductsViewModel>>(productViewModel);
        }
    }
}
