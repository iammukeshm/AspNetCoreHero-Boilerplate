using AspNetCoreHero.Application.Features.Products.Queries.GetAll;
using AspNetCoreHero.Domain.Entities;
using AspNetCoreHero.Web.Models.Shared;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Areas.Products.Pages
{
    public class IndexModel : HeroPageBase<IndexModel>
    {
        public IEnumerable<Product> Products { get; set; }
        public async Task OnGet()
        {
            var response = await Mediator.Send(new GetAllProductsQuery());
            if(response.Succeeded)
            {
                
                var productsViewModel = response.Data;
                Products = Mapper.Map<IEnumerable<Product>>(productsViewModel);
                Logger.LogInformation(Products.Count().ToString());
            }
        }
    }
}
