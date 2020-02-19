using Redis.Library;
using System;

namespace Redis.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ICache redisCache = new RedisCache();

            var userToBeCached = new User
            {
                Id = 1,
                Email = "canertosuner@gmail.com",
                FirstName = "Caner",
                LastName = "Tosuner"
            };
            var key1 = "caner_key1";

            //redisCache.Set(key1, userToBeCached, DateTime.Now.AddMinutes(30));//30 dakikalığına objemizi redis'e atıyoruz

            if (redisCache.Exists(key1))
            {
                //Redis te var olan key'e karşılık gelen value'yu alıp deserialize ettikten sonra return eden metot
                var userRedisResponse = redisCache.Get<User>(key1);

                Console.WriteLine(userRedisResponse.Email);

                //Redis te var olan key-value değerlerini silen metot
                redisCache.Delete(key1);
            }
            else
            {
                redisCache.Set(key1, userToBeCached, DateTime.Now.AddMinutes(30));//30 dakikalığına objemizi redis'e atıyoruz
                //Redis te var olan key'e karşılık gelen value'yu alıp deserialize ettikten sonra return eden metot
                var userRedisResponse = redisCache.Get<User>(key1);

                Console.WriteLine(userRedisResponse.Email);
            }
        }
    }
}
