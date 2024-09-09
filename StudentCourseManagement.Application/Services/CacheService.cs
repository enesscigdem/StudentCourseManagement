using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using StudentCourseManagement.Application.Interfaces;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T> GetCacheAsync<T>(string key) where T : class
    {
        try
        {
            var cachedData = await _cache.GetStringAsync(key);
            if (cachedData != null)
            {
                return JsonSerializer.Deserialize<T>(cachedData);
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cache read failed: {ex.Message}");
            return null;
        }
    }

    public async Task SetCacheAsync<T>(string key, T data, TimeSpan expiration)
    {
        try
        {
            var serializedData = JsonSerializer.Serialize(data);
            await _cache.SetStringAsync(key, serializedData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cache write failed: {ex.Message}");
        }
    }

    public async Task RemoveCacheAsync(string key)
    {
        try
        {
            await _cache.RemoveAsync(key);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Cache remove failed: {ex.Message}");
        }
    }
}