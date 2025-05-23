
using StackExchange.Redis;


namespace Persistence.Repositories
{
    public class CasheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
       readonly IDatabase _database=connection.GetDatabase();
        public async Task<string?> GetAsync(string CashKey)
        {
          var Value= await _database.StringGetAsync(CashKey);
            return Value.IsNullOrEmpty?null:Value.ToString();
        }

        public async Task SetAsync(string CashKey, string CashValue, TimeSpan TimeToLive)
        {
           await _database.StringSetAsync(CashValue, CashValue, TimeToLive);
        }
    }
}
