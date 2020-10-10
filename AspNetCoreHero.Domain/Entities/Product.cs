using AspNetCoreHero.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreHero.Domain.Entities
{
    public class Product : AuditableEntityBase
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public int ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
