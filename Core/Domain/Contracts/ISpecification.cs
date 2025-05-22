﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
   public interface ISpecification<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity,bool>>? Criteria { get;  }

        public List<Expression<Func<TEntity, object>>> IncludeExpression { get;  } 


        public Expression<Func<TEntity, object>> OrderBy { get; }
        public Expression<Func<TEntity ,object>> OrderByDescending {  get; }



        public int Skip {  get; }
        public int Take { get; }

        public bool IsPaginated { get; set; }    
    }
}
