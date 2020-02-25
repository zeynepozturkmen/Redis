using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Redis.Library
{
    public class RedisService : IRedisService  //ToDo: T string'i temsil ediyor.
    {
        private readonly IDatabase _redisDb;
        private readonly ConnectionMultiplexer _connection;


        public RedisService(IDatabase database, ConnectionMultiplexer connection)
        {
            _redisDb = database;
            _connection = connection;
        }

        //Redis te var olan key'e karşılık gelen value'yu alıp deserialize ettikten sonra return eden metot
        public async Task<T> GetAsync<T>(string key)
        {
            var redisObject = await _redisDb.StringGetAsync(key);
            var x = JsonConvert.DeserializeObject<T>(redisObject);
            return x;

        }

        //Redis'e json formatında set işlemi yapılan metot
        public async Task SetAsync<T>(string key, T value, DateTime expireDate)
        {
            var expireTimeSpan = expireDate.Subtract(DateTime.Now);
            await _redisDb.StringSetAsync(key, JsonConvert.SerializeObject(value), expireTimeSpan);
        }

        //Redis te var olan key-value değerlerini silen metot
        public async Task DeleteAsync(string key)
        {
            await _redisDb.KeyDeleteAsync(key);
        }

        //Gönderilen key parametresine göre redis'te bu key var mı yok mu bilgisini return eden metot
        public async Task<bool> ExistsAsync(string key, CommandFlags flags = CommandFlags.None)
        {
            var x = await _redisDb.KeyExistsAsync(key, flags);
            return x;
        }

        //Redis bağlantısını Dispose eden metot
        public void Dispose()
        {
            _connection.Dispose();

        }
    }
}
