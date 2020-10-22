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
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreHero.Application.Features.Products.Queries.GetAll
{

    public class GetAllProductsQuery : IRequest<PagedResponse<GetAllProductsViewModel>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedResponse<GetAllProductsViewModel>>
    {
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IProductRepositoryAsync productRepository,IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<GetAllProductsViewModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            request.PageNumber = request.PageNumber == 0 ? 1 : request.PageNumber;
            request.PageSize = request.PageSize == 0 ? 10 : request.PageSize;
            var products = await _productRepository.GetAllWithCategoriesAsync(request.PageNumber,request.PageSize);
            var count = await _productRepository.Entities.LongCountAsync();
            var productViewModel = _mapper.Map<IEnumerable<GetAllProductsViewModel>>(products.Items);
            return new PagedResponse<GetAllProductsViewModel>(productViewModel.ToList(), count, request.PageNumber, request.PageSize);
        }
    }
}
