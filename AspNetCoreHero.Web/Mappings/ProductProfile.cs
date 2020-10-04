using AspNetCoreHero.Application.Features.Products.Commands.Create;
using AspNetCoreHero.Application.Features.Products.Commands.Update;
using AspNetCoreHero.Application.Features.Products.Queries.GetAll;
using AspNetCoreHero.Domain.Entities;
using AspNetCoreHero.Web.Areas.Products.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Mappings
{
    class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<GetAllProductsViewModel, ProductViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, ProductViewModel>().ReverseMap();
            CreateMap<ProductViewModel, UpdateProductCommand>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
