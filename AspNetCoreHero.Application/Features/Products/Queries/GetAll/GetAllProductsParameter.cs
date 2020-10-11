using AspNetCoreHero.Application.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreHero.Application.Features.Products.Queries.GetAll
{
    public class GetAllProductsParameter : RequestParameter
    {
        public bool ReturnImages { get; set; } = false;
    }
}
