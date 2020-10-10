using AspNetCoreHero.Application.Configurations;
using AspNetCoreHero.Application.Enums.Services;
using AspNetCoreHero.Application.Interfaces.Shared;
using AspNetCoreHero.Infrastructure.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace AspNetCoreHero.Infrastructure.Shared.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailConfiguration>(_config.GetSection("MailConfiguration"));
            services.Configure<CacheConfiguration>(_config.GetSection("MemoryCacheConfiguration"));
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IMailService, MailService>();
            services.AddCaching();
        }
        private static void AddCaching(this IServiceCollection services)
        {
            services.AddTransient<MemoryCacheService>();
            services.AddTransient<RedisCacheService>();
            services.AddTransient<Func<CacheTech, ICacheService>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case CacheTech.Memory:
                        return serviceProvider.GetService<MemoryCacheService>();
                    case CacheTech.Redis:
                        return serviceProvider.GetService<RedisCacheService>();
                    default:
                        return serviceProvider.GetService<MemoryCacheService>();
                }
            });
        }
    }
}
