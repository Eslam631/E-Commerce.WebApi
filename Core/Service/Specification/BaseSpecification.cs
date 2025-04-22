using Domain.Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specification
{
    abstract class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecification(Expression<Func<TEntity, bool>>? criteria) {

            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        #region IncludeExpression
        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; } = [];



        protected void AddInclude(Expression<Func<TEntity, object>> expression) => IncludeExpression.Add(expression);


        #endregion


        #region SortExpression

        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }



        public void AddOrderByAsec(Expression<Func<TEntity, object>> expression) => OrderBy = expression;
        public void AddOrderByDesc(Expression<Func<TEntity, object>> expression) => OrderByDescending = expression;

        #endregion

        #region Pagination

        public int Skip { get; private set; }

        public int Take {  get; private set; }

        public bool IsPaginated { get; set; } 


        protected void ApplyPagination(int PageIndex,int PageSize)
        {
            IsPaginated = true;

            Take = PageSize;
            Skip=(PageIndex - 1)* PageSize;

            
        }

        #endregion

    }
}
