using AspNetCoreHero.Application.Exceptions;
using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Application.Features.Products.Commands.Update
{
    public class UpdateProductCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public decimal Rate { get; set; }
        public int ProductCategoryId { get; set; }
        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<int>>
        {
            private readonly IProductRepositoryAsync _productRepository;
            private readonly IUnitOfWork _unitOfWork;
            public UpdateProductCommandHandler(IProductRepositoryAsync productRepository, IUnitOfWork unitOfWork)
            {
                _productRepository = productRepository;
                _unitOfWork = unitOfWork;
            }
            public async Task<Response<int>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(command.Id);

                if (product == null)
                {
                    throw new ApiException($"Product Not Found.");
                }
                else
                {
                    product.Name = command.Name;
                    product.Rate = command.Rate;
                    product.Description = command.Description;
                    product.Image = command.Image;
                    product.ProductCategoryId = command.ProductCategoryId;
                    await _productRepository.UpdateAsync(product);
                    await _unitOfWork.Commit(cancellationToken);
                    return new Response<int>(product.Id);
                }
            }
        }
    }
}
