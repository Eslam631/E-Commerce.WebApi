using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ICacheRepository
    {
        public Task<string?> GetAsync(string CashKey);

        public Task SetAsync(string CashKey, string CashValue,TimeSpan TimeToLive);
    }
}
