using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Redis.Library
{
    public interface IRedisService : IDisposable
    {
        Task<T> GetAsync<T>(string key);

        Task SetAsync<T>(string key, T value, DateTime expireDate);

        Task DeleteAsync(string key);

        Task<bool> ExistsAsync(string key, CommandFlags flags);
    }
}
