using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface ICacheService
    {

        public Task<string?> GetAsync(string cashKey);

        public Task SetAsync(string cashKey, object cashValue,TimeSpan TimeToLive);


    }
}
