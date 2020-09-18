using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Models.Shared.Modals
{
    public class HeaderViewModel
    {
        public string Title { get; set; }

        public HeaderViewModel(string title)
        {
            Title = title;
        }
    }
}
