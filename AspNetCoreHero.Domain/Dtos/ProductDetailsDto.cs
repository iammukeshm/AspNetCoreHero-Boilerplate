using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreHero.Domain.Dtos
{
    public class ProductDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public string  ProductCategory { get; set; }
    }
}
