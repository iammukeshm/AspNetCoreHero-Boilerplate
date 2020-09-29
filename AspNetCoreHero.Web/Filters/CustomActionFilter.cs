using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Filters
{
    public class CustomActionFilter : ActionFilterAttribute, IActionFilter
    {
        public CustomActionFilter()
        {

        }
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            // TODO: Add your action filter's tasks here

            // Log Action Filter call
            
        }
    }
}
