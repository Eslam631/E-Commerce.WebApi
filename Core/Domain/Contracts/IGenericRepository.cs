using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity :BaseEntity<TKey>
    {
        Task< IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(TKey key);
         Task AddASYNC(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);




    }
}
