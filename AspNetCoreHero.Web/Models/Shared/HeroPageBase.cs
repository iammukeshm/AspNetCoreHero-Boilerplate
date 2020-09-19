using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCoreHero.Web.Models.Shared
{
    public class HeroPageBase<T> : PageModel where T : class
    {
        private IMediator _mediator;
        private IMapper _mapper;
        private ILogger<T> _logger;
        private IMemoryCache _memoryCache;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();
        protected ILogger<T> Logger => _logger ??= HttpContext.RequestServices.GetService<ILogger<T>>();
        protected IMemoryCache MemoryCache => _memoryCache ??= HttpContext.RequestServices.GetService<IMemoryCache>();


    }
}
