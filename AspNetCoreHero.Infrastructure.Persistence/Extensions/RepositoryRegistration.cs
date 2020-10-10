using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreHero.Infrastructure.Persistence.Extensions
{
    public static class RepositoryRegistration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IProductRepositoryAsync, ProductRepositoryAsync>();
            services.AddTransient<IProductCategoryRepositoryAsync, ProductCategoryRepositoryAsync>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            #endregion
        }
    }
}
