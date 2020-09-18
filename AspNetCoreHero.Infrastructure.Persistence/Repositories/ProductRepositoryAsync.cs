using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Domain.Entities;
using AspNetCoreHero.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreHero.Infrastructure.Persistence.Repositories
{
    public class ProductRepositoryAsync : GenericRepositoryAsync<Product>, IProductRepositoryAsync
    {
        private readonly DbSet<Product> _products;

        public ProductRepositoryAsync(ApplicationContext dbContext) : base(dbContext)
        {
            _products = dbContext.Set<Product>();
        }
    }
}
