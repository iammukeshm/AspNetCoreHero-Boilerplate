using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCoreHero.Application.Interfaces.Shared
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
    }
}
