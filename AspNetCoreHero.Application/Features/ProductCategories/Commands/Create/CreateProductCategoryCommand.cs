using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Application.Wrappers;
using AspNetCoreHero.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Application.Features.ProductCategories.Commands.Create
{
    public partial class CreateProductCategoryCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public decimal Tax { get; set; }
    }
    public class CreateProductCategoryCommandHandler : IRequestHandler<CreateProductCategoryCommand, Response<int>>
    {
        private readonly IProductCategoryRepositoryAsync _productCategoryRepository;
        private readonly IMapper _mapper;

        private IUnitOfWork _unitOfWork { get; set; }
        public CreateProductCategoryCommandHandler(IProductCategoryRepositoryAsync productCategoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<ProductCategory>(request);
            await _productCategoryRepository.AddAsync(category);
            return new Response<int>(await _unitOfWork.Commit(cancellationToken));
        }
    }
}
