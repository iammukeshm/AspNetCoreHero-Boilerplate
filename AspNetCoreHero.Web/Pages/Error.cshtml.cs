using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AspNetCoreHero.Web.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int code)
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            Code = code;
            switch(code)
            {
                case 404:
                    Message = "Page Not Found! Please ensure that you have entered the correct URL.";
                    break;
                default:
                    Message = "An Error has occcured.";
                    break;
            }
        }
    }
}
