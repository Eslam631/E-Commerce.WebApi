using Domain.Contracts;
using Domain.Models;
using Persistence.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork(ApplicationDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _Repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
           //GetType
           var TypeName= typeof(TEntity).Name;

            //if (_Repositories.ContainsKey(TypeName))
            //    return(IGenericRepository<TEntity,TKey>) _Repositories[TypeName] ;

            if (_Repositories.TryGetValue(TypeName, out object? value))
                return (IGenericRepository<TEntity, TKey>)value;

            else
            {
                //CreateObject
                var Repo = new GenericRepository<TEntity, TKey>(_dbContext);

                //stored In Dictionary

                _Repositories[TypeName] = Repo;

                return Repo;

            }
         
        }

        public async Task<int> SaveChangeAsync()
        {
          return await _dbContext.SaveChangesAsync();
        }
    }
}
