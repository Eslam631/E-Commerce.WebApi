using Shared.DataTransferObject.BasketDTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
  public interface IBasketService
    {

        public Task<BasketDto> GetBasketAsync(string Key);

        public Task<bool> DeleteBasketAsync(string id);

        public Task<BasketDto> CreateOrUpdateBasketAsync( BasketDto basket);
    }
}
