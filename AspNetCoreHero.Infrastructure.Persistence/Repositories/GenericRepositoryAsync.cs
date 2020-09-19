using AspNetCoreHero.Application.Configurations;
using AspNetCoreHero.Application.Interfaces.Repositories;
using AspNetCoreHero.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreHero.Infrastructure.Persistence.Repositories
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly ApplicationContext _dbContext;
        private MemoryCacheEntryOptions _cacheOptions;
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheConfiguration _cacheConfig;

        public GenericRepositoryAsync(ApplicationContext dbContext, IMemoryCache memoryCache , IOptions<MemoryCacheConfiguration> cacheConfig)
        {
            _dbContext = dbContext;
            _memoryCache = memoryCache;
            _cacheConfig = cacheConfig.Value;
            if(_cacheConfig!=null)
            {
                _cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(_cacheConfig.AbsoluteExpirationInHours),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(_cacheConfig.SlidingExpirationInMinutes)
                };
            }
           
            
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            var cacheKey = $"{typeof(T)}.{nameof(GetAllAsync)}";
            if (!_memoryCache.TryGetValue(cacheKey, out IReadOnlyList<T> cachedList))
            {
                cachedList = await _dbContext
                 .Set<T>()
                 .ToListAsync();
                _memoryCache.Set(cacheKey, cachedList, _cacheOptions);
            }
            return cachedList;
        }
    }
}
