using AspNetCoreHero.Application.Exceptions;
using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Application.Features.ProductCategories.Commands.Update
{
    public class UpdateProductCategoryCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Tax { get; set; }
        public class UpdateProductCategoryCommandHandler : IRequestHandler<UpdateProductCategoryCommand, Response<int>>
        {
            private readonly IProductCategoryRepositoryAsync _productCategoryRepository;
            private readonly IUnitOfWork _unitOfWork;
            public UpdateProductCategoryCommandHandler(IProductCategoryRepositoryAsync productCategoryRepository, IUnitOfWork unitOfWork)
            {
                _productCategoryRepository = productCategoryRepository;
                _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            }
            public async Task<Response<int>> Handle(UpdateProductCategoryCommand command, CancellationToken cancellationToken)
            {
                var category = await _productCategoryRepository.GetByIdAsync(command.Id);

                if (category == null)
                {
                    throw new ApiException($"Product Category Not Found.");
                }
                else
                {
                    category.Name = command.Name;
                    category.Tax = command.Tax;
                    await _productCategoryRepository.UpdateAsync(category);
                    await _unitOfWork.Commit(cancellationToken);
                    return new Response<int>(category.Id);
                }
            }
        }
    }
}
