using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Redis.Library;
using StackExchange.Redis;

namespace Redis.WebApi.Controllers
{
    [Route("api/redisTest")]
    [ApiController]
    public class RedisTestController : ControllerBase
    {

        private readonly IRedisService _redisService;

        public RedisTestController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get()        
        {
            var userToBeCached = new User
            {
                Id = 1,
                Email = "caner@gmail.com",
                FirstName = "Caner",
                LastName = "Tosuner"
            };
            var key1 = "caner_key1";


            if (await _redisService.ExistsAsync(key1, CommandFlags.None))
            {
                //Redis te var olan key'e karşılık gelen value'yu alıp deserialize ettikten sonra return eden metot
                var userRedisResponse =await _redisService.GetAsync<User>(key1);

                //Redis te var olan key-value değerlerini silen metot
                //_redisService.Delete(key1);

                return Ok(userRedisResponse);
            }
            else
            {
               await _redisService.SetAsync(key1, userToBeCached, DateTime.Now.AddMinutes(30));//30 dakikalığına objemizi redis'e atıyoruz

                //Redis te var olan key'e karşılık gelen value'yu alıp deserialize ettikten sonra return eden metot
                var userRedisResponse = _redisService.GetAsync<User>(key1);

                return Ok(userRedisResponse);
            }


        }

    }
}