using System.Collections.Generic;
using System.Threading.Tasks;
using StudentCourseManagement.Domain.Entities;

namespace StudentCourseManagement.Application.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetCacheAsync<T>(string key) where T : class;
        Task SetCacheAsync<T>(string key, T data, TimeSpan expiration);
        Task RemoveCacheAsync(string key);
    }


}

