using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Census.API.Infrastructure.Pagination
{
    public class PagedList<TEntity> where TEntity : class
    {
        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public long Count { get; private set; }

        public IEnumerable<TEntity> Items { get; private set; }

        public PagedList() : this(0, 0, 0, new List<TEntity>()) { }

        public PagedList(int pageIndex, int pageSize, long count, IEnumerable<TEntity> items)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.Count = count;
            this.Items = items;
        }
    }
}
