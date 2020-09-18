using System;

namespace AspNetCoreHero.Application.Interfaces.Shared
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
    }
}
