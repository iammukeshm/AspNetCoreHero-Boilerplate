using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreHero.Application.Features.Products.Queries.GetAll
{
    public class GetAllProductsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public decimal Rate { get; set; }
    }
}
