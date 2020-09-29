using Microsoft.AspNetCore.Builder;

namespace AspNetCoreHero.Infrastructure.Shared.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseSwaggerService(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/V1/swagger.json", "ASP.NET Core Hero - Boilerplate");
                options.RoutePrefix = "swagger";
                options.DisplayRequestDuration();
            });
        }
    }
}
