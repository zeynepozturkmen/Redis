using System;
using System.Collections.Generic;
using System.Text;

namespace Redis.Library
{
    public interface ICache : IDisposable
    {
        T Get<T>(string key);

        void Set<T>(string key, T obj, DateTime expireDate);

        void Delete(string key);

        bool Exists(string key);
    }
}
