using AspNetCoreHero.Application.Extensions;
using AspNetCoreHero.Application.Interfaces.Shared;
using AspNetCoreHero.Infrastructure.Persistence.Extensions;
using AspNetCoreHero.Infrastructure.Shared.Extensions;
using AspNetCoreHero.PublicAPI.Extensions;
using AspNetCoreHero.PublicAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreHero.PublicAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer();
            services.AddSharedInfrastructure(_configuration);
            services.AddApiVersioningExtension();
            services.AddSwaggerService();
            services.AddControllers();
            services.AddPersistenceInfrastructureForApi(_configuration);
            services.AddHttpContextAccessor();
            //For In-Memory Caching
            services.AddMemoryCache();
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwaggerService();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
