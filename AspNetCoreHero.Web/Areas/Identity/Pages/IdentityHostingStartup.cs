using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(AspNetCoreHero.Web.Areas.Identity.IdentityHostingStartup))]
namespace AspNetCoreHero.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}