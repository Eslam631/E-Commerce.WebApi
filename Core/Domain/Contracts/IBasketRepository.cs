using Domain.Models.BasketModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        public Task<CustomerBasket?> GetBasketAsync(string Key);

        public Task<bool> DeleteBasketAsync(string id);

        public Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket customerBasket,TimeSpan? timeToLive=null);
    }
}
