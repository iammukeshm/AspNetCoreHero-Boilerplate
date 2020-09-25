using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Services
{
    public interface IViewRenderService
    {
        Task<string> RenderPartialToStringAsync<T>(
                             string viewName, T model);
    }
}
