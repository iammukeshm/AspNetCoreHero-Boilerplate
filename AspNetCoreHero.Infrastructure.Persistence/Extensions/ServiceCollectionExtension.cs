using AspNetCoreHero.Application.Configurations;
using AspNetCoreHero.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreHero.Infrastructure.Persistence.Extensions
{
    public static class StartupServiceExtension
    {
        public static void AddPersistenceInfrastructureForWeb(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddPersistenceContexts(configuration);

            services.Configure<JWTConfiguration>(configuration.GetSection("JWTConfiguration"));

        }
        public static void AddAuthenticationSchemeForWeb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc(o =>
            {
                //Add Authentication to all Controllers by default.
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                o.Filters.Add(new AuthorizeFilter(policy));
            });

        }
        private static void AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
                           options.UseSqlServer(
                               configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<IdentityContext>()
                    .AddDefaultUI()
            .AddDefaultTokenProviders();
        }
    }
}
