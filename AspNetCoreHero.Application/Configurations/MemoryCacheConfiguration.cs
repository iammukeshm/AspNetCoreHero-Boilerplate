using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreHero.Application.Configurations
{
    public class MemoryCacheConfiguration
    {
        public int AbsoluteExpirationInHours { get; set; }
        public int SlidingExpirationInMinutes { get; set; }
    }
}
