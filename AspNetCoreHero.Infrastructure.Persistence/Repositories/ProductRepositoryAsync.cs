using AspNetCoreHero.Application.Enums.Services;
using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Application.Interfaces.Shared;
using AspNetCoreHero.Application.Wrappers;
using AspNetCoreHero.Domain.Dtos;
using AspNetCoreHero.Domain.Entities;
using AspNetCoreHero.Infrastructure.Persistence.Contexts;
using AspNetCoreHero.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AspNetCoreHero.Infrastructure.Persistence.Repositories
{
    public class ProductRepositoryAsync : GenericRepositoryAsync<Product>, IProductRepositoryAsync
    {
        private readonly static CacheTech cacheTech = CacheTech.Memory;
        private readonly string cacheKey = $"{typeof(Product)}";
        private readonly Func<CacheTech, ICacheService> _cacheService;
        private readonly DbSet<Product> _products;

        public ProductRepositoryAsync(ApplicationContext dbContext, Func<CacheTech, ICacheService> cacheService) : base(dbContext, cacheService)
        {
            _products = dbContext.Set<Product>();
            _cacheService = cacheService;
        }

        public async Task<PagedResponse<Product>> GetAllWithCategoriesAsync(int pageNumber, int pageSize, bool isCached = false)
        {
            try
            {
                var paginatedList = await _products
                    .Include(a => a.ProductCategory)
                    .Select(o => new Product
                    {
                        Barcode = o.Barcode,
                        Name = o.Name,
                        ProductCategory = o.ProductCategory,
                        ProductCategoryId = o.ProductCategoryId,
                        Rate = o.Rate,
                        Description = o.Description,
                        Id = o.Id
                    })
                    .ToPaginatedListAsync<Product>(pageNumber, pageSize);
                return paginatedList;
            }
            catch (Exception ex)
            {

                throw;
            }



        }

        public async Task<IReadOnlyList<Product>> GetAllWithCategoriesWithoutImagesAsync(bool isCached = false)
        {
            if (!_cacheService(cacheTech).TryGet($"{cacheKey}WithoutImages", out IReadOnlyList<Product> cachedList))
            {
                var data = _products.Include(a => a.ProductCategory).AsQueryable();
                cachedList = await data.Select(a =>
                new Product
                {
                    Id = a.Id,
                    Description = a.Description,
                    Name = a.Name,
                    ProductCategory = a.ProductCategory,
                    ProductCategoryId = a.ProductCategoryId,
                    Rate = a.Rate,
                    Barcode = a.Barcode
                }).ToListAsync();
                _cacheService(cacheTech).Set($"{cacheKey}WithoutImages", cachedList);
            }
            return cachedList;
        }

        public Task<bool> IsUniqueBarcodeAsync(string barcode)
        {
            return _products.AllAsync(p => p.Barcode != barcode);
        }
    }
}
