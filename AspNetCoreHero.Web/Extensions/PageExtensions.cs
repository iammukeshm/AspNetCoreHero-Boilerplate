using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace AspNetCoreHero.Web.Extensions
{
    public static class PageExtensions
    {
        public static async Task<string> RenderViewAsync(this PageModel pageModel, string pageName)
        {
            var actionContext = new ActionContext(
                pageModel.HttpContext,
                pageModel.RouteData,
                pageModel.PageContext.ActionDescriptor
            );

            using (var sw = new StringWriter())
            {
                IRazorViewEngine _razorViewEngine = pageModel.HttpContext.RequestServices.GetService(typeof(IRazorViewEngine)) as IRazorViewEngine;
                IRazorPageActivator _activator = pageModel.HttpContext.RequestServices.GetService(typeof(IRazorPageActivator)) as IRazorPageActivator;

                var result = _razorViewEngine.FindPage(actionContext, pageName);

                if (result.Page == null)
                {
                    throw new ArgumentNullException($"The page {pageName} cannot be found.");
                }

                var page = result.Page;

                var view = new RazorView(_razorViewEngine,
                    _activator,
                    new List<IRazorPage>(),
                    page,
                    HtmlEncoder.Default,
                    new DiagnosticListener("ViewRenderService"));


                var viewContext = new ViewContext(
                    actionContext,
                    view,
                    pageModel.ViewData,
                    pageModel.TempData,
                    sw,
                    new HtmlHelperOptions()
                );


                var pageNormal = ((Page)result.Page);

                pageNormal.PageContext = pageModel.PageContext;

                pageNormal.ViewContext = viewContext;


                _activator.Activate(pageNormal, viewContext);

                await page.ExecuteAsync();

                return sw.ToString();
            }
        }
    }
}
