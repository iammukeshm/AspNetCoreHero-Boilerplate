using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Areas.Products.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepositoryAsync _productRepositoryAsync;

        public IndexModel(IProductRepositoryAsync productRepositoryAsync)
        {
            this._productRepositoryAsync = productRepositoryAsync;
        }
        public IEnumerable<Product> Products { get; set; }
        public async Task OnGet()
        {
            Products = await _productRepositoryAsync.GetAllAsync();
        }
    }
}
