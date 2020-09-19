using AspNetCoreHero.Application.Features.Products.Commands.Create;
using AspNetCoreHero.Application.Features.Products.Queries.GetAll;
using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Domain.Entities;
using AspNetCoreHero.Web.Models.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Areas.Products.Pages
{
    public class IndexModel : HeroPageBase<IndexModel>
    {
        public CreateProductCommand createModel { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public async Task OnGet()
        {
            var response = await Mediator.Send(new GetAllProductsQuery());
            if(response.Succeeded)
            {
                var productsViewModel = response.Data;
                Products = Mapper.Map<IEnumerable<Product>>(productsViewModel);
            }
            
        }
        public async Task<IActionResult> OnPostCreateProductAsync(CreateProductCommand createModel)
        {
           var result = await Mediator.Send(createModel);
            return RedirectToPage("Index");
        }
    }
}
