using Domain.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service
{
    public class CacheService(ICacheRepository cashRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string cashKey) =>await cashRepository.GetAsync(cashKey);
        

        public async Task SetAsync(string cashKey, object cashValue, TimeSpan TimeToLive)
        {
            var value=JsonSerializer.Serialize(cashValue);

            await cashRepository.SetAsync(cashKey, value, TimeToLive);

        }
    }
}
