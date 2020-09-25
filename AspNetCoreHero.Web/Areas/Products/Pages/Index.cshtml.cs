using AspNetCoreHero.Application.Features.Products.Commands.Create;
using AspNetCoreHero.Application.Features.Products.Queries.GetAll;
using AspNetCoreHero.Application.Features.Products.Queries.GetById;
using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Domain.Entities;
using AspNetCoreHero.Web.Areas.Products.ViewModels;
using AspNetCoreHero.Web.Extensions;
using AspNetCoreHero.Web.Models.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Areas.Products.Pages
{
    public class IndexModel : HeroPageBase<IndexModel>
    {
        public ProductViewModel Product { get; set; } = new ProductViewModel();
        public IEnumerable<ProductViewModel> Products { get; set; }
        public async Task OnGet()
        {


        }
        public async Task<PartialViewResult> OnGetProductsPartial()
        {
            var response = await Mediator.Send(new GetAllProductsQuery());
            if (response.Succeeded)
            {
                var data = response.Data;
                Products = Mapper.Map<IEnumerable<ProductViewModel>>(data);
            }
            return new PartialViewResult
            {
                ViewName = "_Products",
                ViewData = new ViewDataDictionary<IEnumerable<ProductViewModel>>(ViewData, Products)
            };
        }
        public async Task<JsonResult> OnGetCreateOrEditAsync(int id = 0)
        {
            if (id == 0)
                return new JsonResult(new { isValid = true, html = await Renderer.RenderPartialToStringAsync<ProductViewModel>("_CreateOrEdit", new ProductViewModel()) });
            else
            {
                var response = await Mediator.Send(new GetProductByIdQuery { Id = id });
                var viewModel = Mapper.Map<ProductViewModel>(response.Data);
                return new JsonResult(new { isValid = true, html = await Renderer.RenderPartialToStringAsync<ProductViewModel>("_CreateOrEdit", viewModel) });
            }


        }
        public async Task<JsonResult> OnPostCreateOrEditAsync(int id, ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    var createProductCommand = Mapper.Map<CreateProductCommand>(product);
                    var result = await Mediator.Send(createProductCommand);
                    
                }
                else
                {
                    //Update
                }
                var response = await Mediator.Send(new GetAllProductsQuery());
                if (response.Succeeded)
                {
                    var data = response.Data;
                    Products = Mapper.Map<IEnumerable<ProductViewModel>>(data);
                }
                var html = await Renderer.RenderPartialToStringAsync("_Products", Products);
                return new JsonResult(new { isValid = true, html = html });
            }
            else
            {
                var html = await Renderer.RenderPartialToStringAsync<ProductViewModel>("_CreateOrEdit", product);
                return new JsonResult(new { isValid = false, html = html });
            }


        }
        public async Task<JsonResult> OnPostDeleteAsync(int id)
        {
            var html = await Renderer.RenderPartialToStringAsync<ProductViewModel>("_Products", new ProductViewModel());
            return new JsonResult(new { isValid = true, html = html });
        }

    }
}
