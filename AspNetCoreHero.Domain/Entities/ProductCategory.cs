using AspNetCoreHero.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreHero.Domain.Entities
{
    public class ProductCategory : AuditableEntityBase
    {
        public string Name { get; set; }
        public decimal Tax { get; set; }
    }
}
