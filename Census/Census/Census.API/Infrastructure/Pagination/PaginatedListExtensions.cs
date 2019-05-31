using Census.API.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Census.API.Infrastructure.Pagination
{
    public static class PaginatedListExtensions
    {
        public static async Task<PagedList<TEntity>> ToPagedListAsync<TEntity>(
            this IQueryable<TEntity> items, int pageIndex, int pageSize) 
            where TEntity : class, new()
        {
            var totalCount = items.LongCount();
            var filteredItems = await items.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            if (filteredItems.Count == 0 & totalCount > 0)
            {
                throw new InvalidInputException($"Page index is out of range");
            }

            return new PagedList<TEntity>(pageIndex, pageSize, totalCount, filteredItems);
        }
    }
}
