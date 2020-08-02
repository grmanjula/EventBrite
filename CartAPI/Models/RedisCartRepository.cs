using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CartAPI.Models
{
    public class RedisCartRepository : ICartRepository
    {
        
        private readonly ConnectionMultiplexer _redis;
        //Below statement is to create Redis version of the Database
        private readonly IDatabase _database;

        //Here you are injecting the connection Multiplexer/Redis Cache. Startup will inject me this which will tell me where Redis is
        public RedisCartRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = redis.GetDatabase();
          // Above you are getting DB from Redis to store your contents

        }
        public async Task<bool> DeleteCartAsync(string id)
        {
          return await  _database.KeyDeleteAsync(id);
        }

        public async Task<Cart> GetCartAsync(string cartId)
        {
           var data = await _database.StringGetAsync(cartId);
            if(data.IsNullOrEmpty)
            {
                return null;
            }
           return  JsonConvert.DeserializeObject<Cart>(data);
            //This is a way of converting data into DeserializedObject called as Cart and return back to column
            
        }

        public IEnumerable<string> GetUsers()
        {
            var server = GetServer();
            var data = server.Keys();
            //if the data is not null only then it moves forward. If it empty if you do an operation it is going to fail with NRE
            return data?.Select(k => k.ToString());
        }

        private IServer GetServer()
        {
            //Get me all the end points where Redis is hosted for my application
           var endpoints = _redis.GetEndPoints();
           return  _redis.GetServer(endpoints.First());
        }

        public async Task<Cart> UpdateCartAsync(Cart basket)
        {
            //StringSet says go set it by giving Key Value pair. The below line creates Cache for me and create if it doesn't exist
           var created = await  _database.StringSetAsync(basket.BuyerId, JsonConvert.SerializeObject(basket));
            if(!created)
            {
                return null;
            }
            //After I am able to successfully write the data in the DB I am going to call GetCartAsync method to locate my Cart and retrn back
            return await GetCartAsync(basket.BuyerId);
        }
    }
}
