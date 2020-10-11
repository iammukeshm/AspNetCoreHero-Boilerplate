using AspNetCoreHero.Application.Features.ProductCategories.Queries.GetAll;
using AspNetCoreHero.Application.Features.Products.Commands.Create;
using AspNetCoreHero.Application.Features.Products.Commands.Update;
using AspNetCoreHero.Application.Features.Products.Queries.GetAll;
using AspNetCoreHero.Domain.Entities;
using AspNetCoreHero.Web.Areas.Products.ViewModels;
using AutoMapper;

namespace AspNetCoreHero.Web.Mappings
{
    class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<GetAllProductsViewModel, ProductViewModel>().ReverseMap();
            CreateMap<GetAllProductCategoryViewModel, ProductCategoryViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, ProductViewModel>().ReverseMap();
            CreateMap<ProductViewModel, UpdateProductCommand>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryViewModel>();
        }
    }
}
