using AspNetCoreHero.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Application.Interfaces.Repositories
{
    public interface IProductRepositoryAsync : IGenericRepositoryAsync<Product>
    {
        Task<IReadOnlyList<Product>> GetAllWithCategoriesAsync(bool isCached = false);
        Task<IReadOnlyList<Product>> GetAllWithCategoriesWithoutImagesAsync(bool isCached = false);
        Task<bool> IsUniqueBarcodeAsync(string barcode);
    }
}
