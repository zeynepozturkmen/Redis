using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redis.Library
{
    public class RedisCache : ICache
    {
        private readonly IDatabase _redisDb;

        //Connection bilgisi initialize anında alınıyor
        public RedisCache()
        {
            _redisDb = RedisConnectionFactory.Connection.GetDatabase();
        }
        //Redis'e json formatında set işlemi yapılan metot
        public void Set<T>(string key, T objectToCache, DateTime expireDate)
        {
            var expireTimeSpan = expireDate.Subtract(DateTime.Now);

            _redisDb.StringSet(key, JsonConvert.SerializeObject(objectToCache), expireTimeSpan);
        }

        //Redis te var olan key'e karşılık gelen value'yu alıp deserialize ettikten sonra return eden metot
        public T Get<T>(string key)
        {
            var redisObject = _redisDb.StringGet(key);

            return redisObject.HasValue ? JsonConvert.DeserializeObject<T>(redisObject) : Activator.CreateInstance<T>();
        }

        //Redis te var olan key-value değerlerini silen metot
        public void Delete(string key)
        {
            _redisDb.KeyDelete(key);
        }

        //Gönderilen key parametresine göre redis'te bu key var mı yok mu bilgisini return eden metot
        public bool Exists(string key)
        {
            return _redisDb.KeyExists(key);
        }

        //Redis bağlantısını Dispose eden metot
        public void Dispose()
        {
            RedisConnectionFactory.Connection.Dispose();
        }
    }
    }
