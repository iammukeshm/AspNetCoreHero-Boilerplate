using AspNetCoreHero.Application.Constants.Permissions;
using AspNetCoreHero.Application.Features.ProductCategories.Queries.GetAll;
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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
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
        public async Task<PartialViewResult> OnGetViewAll(int pageNumber, int pageSize)
        {
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            var response = await Mediator.Send(new GetAllProductsQuery() { PageSize = pageSize, PageNumber = pageNumber });
            if (response.Succeeded)
            {
                var data = response.Items;
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
            {
                var categories = await Mediator.Send(new GetAllProductCategoriesQuery());
                var viewModel = new ProductViewModel();
                var categoriesViewModel = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(categories.Data);
                viewModel.ProductCategories = new SelectList(categoriesViewModel, nameof(ProductCategoryViewModel.Id), nameof(ProductCategoryViewModel.Name), null,null);
                return new JsonResult(new { isValid = true, html = await Renderer.RenderPartialToStringAsync<ProductViewModel>("_CreateOrEdit", viewModel) });
            }
            else
            {

                var categories = await Mediator.Send(new GetAllProductCategoriesQuery());
                var response = await Mediator.Send(new GetProductByIdQuery { Id = id });
                var viewModel = Mapper.Map<ProductViewModel>(response.Data);
                var categoriesViewModel = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(categories.Data);
                viewModel.ProductCategories = new SelectList(categoriesViewModel, nameof(ProductCategoryViewModel.Id), nameof(ProductCategoryViewModel.Name), null, null);
                return new JsonResult(new { isValid = true, html = await Renderer.RenderPartialToStringAsync<ProductViewModel>("_CreateOrEdit", viewModel) });
            }
        }
        public async Task<JsonResult> OnPostCreateOrEditAsync(int id, ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Request.Form.Files.Count > 0)
                    {
                        IFormFile file = Request.Form.Files.FirstOrDefault();
                        product.Image = file.OptimizeImageSize(700, 700);
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
                        if (product.Image == null)
                        {
                            var oldProduct = await Mediator.Send(new GetProductByIdQuery { Id = id });
                            product.Image = oldProduct.Data.Image;
                        }
                        var updateProductCommand = Mapper.Map<UpdateProductCommand>(product);

                        try
                        {
                            var result = await Mediator.Send(updateProductCommand);
                            if (result.Succeeded) Notify.AddSuccessToastMessage($"Product Updated.");
                        }
                        catch (Exception ex)
                        {
                            Logger.LogInformation(ex.Message);
                            throw;
                        }

                    }
                    var response = await Mediator.Send(new GetAllProductsQuery());
                    if (response.Succeeded)
                    {
                        var data = response.Items;
                        Products = Mapper.Map<IEnumerable<ProductViewModel>>(data);
                    }
                    var html = await Renderer.RenderPartialToStringAsync("_ViewAll", Products);
                    return new JsonResult(new { isValid = true, html = html });
                }
                catch (Exception ex)
                {

                    Notify.AddErrorToastMessage(ex.Message);
                    throw;
                }
                
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
                
                var data = response.Items;
                Products = Mapper.Map<IEnumerable<ProductViewModel>>(data);
            }
            var html = await Renderer.RenderPartialToStringAsync("_ViewAll", Products);
            return new JsonResult(new { isValid = true, html = html });
        }

    }
}
