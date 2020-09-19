using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreHero.Application.Interfaces.Shared
{
    public interface ICacheService
    {
        bool TryGetCache<T>(string cacheKey, out T value);
        T TrySetCache<T>(object key, T value);
    }
}
