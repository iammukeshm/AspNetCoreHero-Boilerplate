using AspNetCoreHero.Application.Exceptions;
using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Application.Features.Products.Commands.Delete
{
    public class DeleteProductByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, Response<int>>
        {
            private readonly IProductRepositoryAsync _productRepository;
            private readonly IUnitOfWork _unitOfWork;
            public DeleteProductByIdCommandHandler(IProductRepositoryAsync productRepository, IUnitOfWork unitOfWork)
            {
                _productRepository = productRepository;
                _unitOfWork = unitOfWork;
            }
            public async Task<Response<int>> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(command.Id);
                if (product == null) throw new ApiException($"Product Not Found.");
                await _productRepository.DeleteAsync(product);
                await _unitOfWork.Commit(cancellationToken);
                return new Response<int>(product.Id);
            }
        }
    }
}
