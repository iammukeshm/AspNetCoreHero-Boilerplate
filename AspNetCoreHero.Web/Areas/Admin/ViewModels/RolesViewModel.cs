using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Areas.Admin.ViewModels
{
    public class RolesViewModel
    {
        [Required]
        public string Name { get; set; }
        public string Id { get; set; }
    }
}
