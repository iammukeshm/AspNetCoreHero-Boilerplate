using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreHero.Application.Features.ProductCategories.Queries.GetAll
{
    public class GetAllProductCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Tax { get; set; }
    }
}
