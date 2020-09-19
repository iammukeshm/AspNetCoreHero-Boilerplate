using AspNetCoreHero.Application.Configurations;
using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Domain.Entities;
using AspNetCoreHero.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreHero.Infrastructure.Persistence.Repositories
{
    public class ProductRepositoryAsync : GenericRepositoryAsync<Product>, IProductRepositoryAsync
    {
        private readonly DbSet<Product> _products;

        public ProductRepositoryAsync(ApplicationContext dbContext,IMemoryCache memoryCache,IOptions<MemoryCacheConfiguration> memoryCacheConfig) : base(dbContext, memoryCache, memoryCacheConfig)
        {
            _products = dbContext.Set<Product>();
        }
    }
}
