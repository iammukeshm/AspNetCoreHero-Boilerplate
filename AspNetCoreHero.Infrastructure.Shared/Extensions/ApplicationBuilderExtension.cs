using Microsoft.AspNetCore.Builder;

namespace AspNetCoreHero.Infrastructure.Shared.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseSwaggerService(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/v1/swagger.json", "ASP.NET Core Hero - Boilerplate");
                setupAction.RoutePrefix = "swagger";
            });
        }
    }
}
