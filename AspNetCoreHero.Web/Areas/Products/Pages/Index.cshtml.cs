using AspNetCoreHero.Application.Constants.Permissions;
using AspNetCoreHero.Application.Features.Products.Commands.Create;
using AspNetCoreHero.Application.Features.Products.Commands.Delete;
using AspNetCoreHero.Application.Features.Products.Commands.Update;
using AspNetCoreHero.Application.Features.Products.Queries.GetAll;
using AspNetCoreHero.Application.Features.Products.Queries.GetById;
using AspNetCoreHero.Web.Areas.Products.ViewModels;
using AspNetCoreHero.Web.Extensions;
using AspNetCoreHero.Web.Helpers;
using AspNetCoreHero.Web.Models.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Areas.Products.Pages
{
    public class IndexModel : HeroPageModel<IndexModel>
    {
        public IEnumerable<ProductViewModel> Products { get; set; }
        public void OnGet()
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
                ViewName = "_ViewAll",
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
                if (Request.Form.Files.Count > 0)
                {
                    IFormFile file = Request.Form.Files.FirstOrDefault();
                    using (var dataStream = new MemoryStream())
                    {
                        await file.CopyToAsync(dataStream);
                        product.Image = dataStream.ToArray();
                    }
                }

                if (id == 0)
                {
                    User.HasRequiredClaims(new List<string> { MasterPermissions.Create, ProductPermissions.Create });
                    var createProductCommand = Mapper.Map<CreateProductCommand>(product);
                    var result = await Mediator.Send(createProductCommand);
                    if (result.Succeeded) Notify.AddSuccessToastMessage($"Product Created.");
                }
                else
                {
                    User.HasRequiredClaims(new List<string> { MasterPermissions.Update, ProductPermissions.Update });
                    if(product.Image==null)
                    {
                        var oldProduct = await Mediator.Send(new GetProductByIdQuery { Id = id });
                        product.Image = oldProduct.Data.Image;
                    }
                    var updateProductCommand = Mapper.Map<UpdateProductCommand>(product);
                    var result = await Mediator.Send(updateProductCommand);
                    if (result.Succeeded) Notify.AddSuccessToastMessage($"Product Updated.");
                }
                var response = await Mediator.Send(new GetAllProductsQuery());
                if (response.Succeeded)
                {
                    var data = response.Data;
                    Products = Mapper.Map<IEnumerable<ProductViewModel>>(data);
                }
                var html = await Renderer.RenderPartialToStringAsync("_ViewAll", Products);
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
            User.HasRequiredClaims(new List<string> { MasterPermissions.Delete, ProductPermissions.Delete });
            var thisProduct = await Mediator.Send(new DeleteProductByIdCommand { Id = id });
            Notify.AddInfoToastMessage($"Product with Id {id} Deleted.");
            var response = await Mediator.Send(new GetAllProductsQuery());
            if (response.Succeeded)
            {
                
                var data = response.Data;
                Products = Mapper.Map<IEnumerable<ProductViewModel>>(data);
            }
            var html = await Renderer.RenderPartialToStringAsync("_ViewAll", Products);
            return new JsonResult(new { isValid = true, html = html });
        }

    }
}
