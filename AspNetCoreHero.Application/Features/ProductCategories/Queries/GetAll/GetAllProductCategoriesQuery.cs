using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Application.Features.ProductCategories.Queries.GetAll
{
    public class GetAllProductCategoriesQuery : IRequest<Response<IEnumerable<GetAllProductCategoryViewModel>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductCategoriesQuery, Response<IEnumerable<GetAllProductCategoryViewModel>>>
    {
        private readonly IProductCategoryRepositoryAsync _productCategoryRepository;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IProductCategoryRepositoryAsync productCategoryRepository, IMapper mapper)
        {
            _productCategoryRepository = productCategoryRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetAllProductCategoryViewModel>>> Handle(GetAllProductCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _productCategoryRepository.GetAllAsync();
            var categoriesViewModel = _mapper.Map<IEnumerable<GetAllProductCategoryViewModel>>(categories);
            return new Response<IEnumerable<GetAllProductCategoryViewModel>>(categoriesViewModel);
        }
    }
}
