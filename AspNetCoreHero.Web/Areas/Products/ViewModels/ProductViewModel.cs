using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Areas.Products.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(12, ErrorMessage = "Maximum 12 characters only")]
        public string Name { get; set; }
        [Required]
        public string Barcode { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(0.01, 100000000, ErrorMessage = "Price must be greter than zero !")]
        public decimal Rate { get; set; }
    }
}
