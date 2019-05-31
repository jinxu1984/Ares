using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Census.API.Dto;
using Census.API.Infrastructure.Pagination;
using Census.API.Model;

namespace Census.API.Extensions
{
    public static class CensusEntityExtension
    {
        public static PagedList<TEntity> ToPageDtos<TEntity>(
            this List<CensusEntity> censusEntities,
            Func<CensusEntity, TEntity> transformFunc,
            int pageIndex, int pageSize)
            where TEntity : class, new()
        {
            PagedList<TEntity> pagedDtos = null;
            if (censusEntities.Count == 0) { return pagedDtos; }

            var selectedDtos = censusEntities
                                .OrderBy(e => e.StateId)
                                .Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .Select(transformFunc);

            pagedDtos = new PagedList<TEntity>(pageIndex, pageIndex,
                censusEntities.Count, selectedDtos);

            return pagedDtos;

        }
    }
}
