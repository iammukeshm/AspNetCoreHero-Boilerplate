using AspNetCoreHero.Application.Features.Products.Commands.Create;
using AspNetCoreHero.Application.Features.Products.Queries.GetAll;
using AspNetCoreHero.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreHero.Application.Mappings
{
    class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
        }
    }
}
