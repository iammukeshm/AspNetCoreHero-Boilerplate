using AspNetCoreHero.Web.Services;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NToastNotify;

namespace AspNetCoreHero.Web.Models.Shared
{
    public class HeroPageModel<T> : PageModel where T : class
    {
        private IMediator _mediator;
        private IMapper _mapper;
        private ILogger<T> _logger;
        private IViewRenderService _viewRenderService;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();
        protected ILogger<T> Logger => _logger ??= HttpContext.RequestServices.GetService<ILogger<T>>();
        protected IViewRenderService Renderer => _viewRenderService ??= HttpContext.RequestServices.GetService<IViewRenderService>();
        private IToastNotification _toastNotification;
        protected IToastNotification Notify => _toastNotification ??= HttpContext.RequestServices.GetService<IToastNotification>();


    }
}
