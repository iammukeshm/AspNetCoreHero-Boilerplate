using AspNetCoreHero.Application.Exceptions;
using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Application.Features.ProductCategories.Commands.Delete
{
    public class DeleteProductCategoryByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteProductCategoryByIdCommandHandler : IRequestHandler<DeleteProductCategoryByIdCommand, Response<int>>
        {
            private readonly IProductCategoryRepositoryAsync _productCategoryRepository;
            private readonly IUnitOfWork _unitOfWork;
            public DeleteProductCategoryByIdCommandHandler(IProductCategoryRepositoryAsync productRepository, IUnitOfWork unitOfWork)
            {
                _productCategoryRepository = productRepository;
                _unitOfWork = unitOfWork;
            }
            public async Task<Response<int>> Handle(DeleteProductCategoryByIdCommand command, CancellationToken cancellationToken)
            {
                var category = await _productCategoryRepository.GetByIdAsync(command.Id);
                if (category == null) throw new ApiException($"Product Group Not Found.");
                await _productCategoryRepository.DeleteAsync(category);
                await _unitOfWork.Commit(cancellationToken);
                return new Response<int>(category.Id);
            }
        }
    }
}
