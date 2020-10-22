using AspNetCoreHero.Application.Wrappers;
using AspNetCoreHero.Domain.Dtos;
using AspNetCoreHero.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Application.Interfaces.Repositories
{
    public interface IProductRepositoryAsync : IGenericRepositoryAsync<Product>
    {
        Task<PagedResponse<Product>> GetAllWithCategoriesAsync(int pageNumber, int pageSize, bool isCached = false);
        Task<bool> IsUniqueBarcodeAsync(string barcode);
    }
}
