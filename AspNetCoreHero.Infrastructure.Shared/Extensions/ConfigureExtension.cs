using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreHero.Infrastructure.Shared.Extensions
{
    public static class ConfigureExtension
    {
        public static void UseSerilog(this ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
        }
    }
}
