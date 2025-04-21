using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> InputQuery,ISpecification<TEntity,TKey> specification) where TEntity :BaseEntity<TKey>
        {
            var Query=InputQuery ;

            if(specification.Criteria is not null)
           
                Query = Query.Where(specification.Criteria);
           if(specification.IncludeExpression is not null&& specification.IncludeExpression.Count>0 ) 

                Query = specification.IncludeExpression.Aggregate(Query, (CurrentQuery, IncludeExp) => CurrentQuery.Include(IncludeExp));



                

           



                return Query;
        }
    }
}
