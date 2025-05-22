using Domain.Models.BasketModel;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository

    {

        private readonly IDatabase _database=connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket customerBasket, TimeSpan? timeToLive = null)
        {
           var BasketJson=JsonSerializer.Serialize(customerBasket);
         var CreateOrUpdate= await  _database.StringSetAsync(customerBasket.Id,BasketJson, timeToLive ?? TimeSpan.FromDays(30));

            if (CreateOrUpdate)
            {
              return customerBasket;
            }
            else
                return null;
        
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
         return await  _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string Key)
        {
          var Basket= await _database.StringGetAsync(Key);
            if(Basket.IsNullOrEmpty )
                return null;
            else
            {
                var BasketJson=JsonSerializer.Deserialize<CustomerBasket>(Basket!);
                return BasketJson;
            }
        }
    }
}
