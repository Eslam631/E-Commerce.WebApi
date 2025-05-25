﻿namespace Shared
{
    public class PaginatedResult<TEntity>
    {
        public PaginatedResult(int pageIndex, int pageSize, int totalCount, IEnumerable<TEntity> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = totalCount;
            Data = data;
        }

        public int PageIndex {  get; set; }
        public int PageSize {  get; set; }
        public int Count { get; set; }

       public IEnumerable<TEntity> Data { get; set; }
    }
}
