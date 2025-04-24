using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.BasketModel;
using ServiceAbstraction;
using Shared.DataTransferObject.BasketDTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BasketService(IBasketRepository _basketRepository,IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
          var Basket=  _mapper.Map<BasketDto, CustomerBasket>(basket);
        var CreateOrUpdate=   await _basketRepository.CreateOrUpdateBasketAsync(Basket);
            if (CreateOrUpdate is not null)
                return await GetBasketAsync(basket.Id);
            else
              throw new Exception("Not Can Create Or Update Now ,Try Again Later");
               
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
         return await  _basketRepository.DeleteBasketAsync(id);
        
        }

        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var Basket=await _basketRepository.GetBasketAsync(Key);

            if (Basket is not null)
                return _mapper.Map<CustomerBasket, BasketDto>(Basket);
            else
                throw new BasketNotFoundException(Key);

        }
    }
}
