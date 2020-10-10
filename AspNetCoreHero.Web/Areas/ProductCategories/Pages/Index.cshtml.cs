using AspNetCoreHero.Application.Constants.Permissions;
using AspNetCoreHero.Application.Features.ProductCategories.Commands.Create;
using AspNetCoreHero.Application.Features.ProductCategories.Commands.Delete;
using AspNetCoreHero.Application.Features.ProductCategories.Commands.Update;
using AspNetCoreHero.Application.Features.ProductCategories.Queries.GetAll;
using AspNetCoreHero.Application.Features.ProductCategories.Queries.GetById;
using AspNetCoreHero.Web.Areas.ProductCategories.ViewModels;
using AspNetCoreHero.Web.Helpers;
using AspNetCoreHero.Web.Models.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Areas.ProductCategories.Pages
{
    public class IndexModel : HeroPageModel<IndexModel>
    {
        public IEnumerable<ProductCategoryViewModel> ProductCategories { get; set; }
        public void OnGet()
        {
        }
        public async Task<PartialViewResult> OnGetViewAll()
        {
            var response = await Mediator.Send(new GetAllProductCategoriesQuery());
            if (response.Succeeded)
            {
                var data = response.Data;
                ProductCategories = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(data);
            }
            return new PartialViewResult
            {
                ViewName = "_ViewAll",
                ViewData = new ViewDataDictionary<IEnumerable<ProductCategoryViewModel>>(ViewData, ProductCategories)
            };
        }
        public async Task<JsonResult> OnGetCreateOrEditAsync(int id = 0)
        {
            if (id == 0)
            {
                var viewModel = new ProductCategoryViewModel();
                return new JsonResult(new { isValid = true, html = await Renderer.RenderPartialToStringAsync<ProductCategoryViewModel>("_CreateOrEdit", viewModel) });
            }
            else
            {

                var response = await Mediator.Send(new GetProductCategoryByIdQuery { Id = id });
                var viewModel = Mapper.Map<ProductCategoryViewModel>(response.Data);
                return new JsonResult(new { isValid = true, html = await Renderer.RenderPartialToStringAsync<ProductCategoryViewModel>("_CreateOrEdit", viewModel) });
            }
        }
        public async Task<JsonResult> OnPostCreateOrEditAsync(int id, ProductCategoryViewModel product)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (id == 0)
                    {
                        User.HasRequiredClaims(new List<string> { MasterPermissions.Create, ProductCategoryPermissions.Create });
                        var createProductCommand = Mapper.Map<CreateProductCategoryCommand>(product);
                        var result = await Mediator.Send(createProductCommand);
                        if (result.Succeeded) Notify.AddSuccessToastMessage($"Product Created.");
                    }
                    else
                    {
                        User.HasRequiredClaims(new List<string> { MasterPermissions.Update, ProductCategoryPermissions.Update });
                        var updateProductCommand = Mapper.Map<UpdateProductCategoryCommand>(product);

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
                    var response = await Mediator.Send(new GetAllProductCategoriesQuery());
                    if (response.Succeeded)
                    {
                        var data = response.Data;
                        ProductCategories = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(data);
                    }
                    var html = await Renderer.RenderPartialToStringAsync("_ViewAll", ProductCategories);
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
                var html = await Renderer.RenderPartialToStringAsync<ProductCategoryViewModel>("_CreateOrEdit", product);
                return new JsonResult(new { isValid = false, html = html });
            }
        }
        public async Task<JsonResult> OnPostDeleteAsync(int id)
        {
            User.HasRequiredClaims(new List<string> { MasterPermissions.Delete, ProductCategoryPermissions.Delete });
            var thisProduct = await Mediator.Send(new DeleteProductCategoryByIdCommand { Id = id });
            Notify.AddInfoToastMessage($"Product Category with Id {id} Deleted.");
            var response = await Mediator.Send(new GetAllProductCategoriesQuery());
            if (response.Succeeded)
            {

                var data = response.Data;
                ProductCategories = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(data);
            }
            var html = await Renderer.RenderPartialToStringAsync("_ViewAll", ProductCategories);
            return new JsonResult(new { isValid = true, html = html });
        }
    }
}
