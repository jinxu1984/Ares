using Census.API.Infrastructure;
using Census.API.Infrastructure.Pagination;
using Census.API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Census.API.Repositories
{
    public class CensusRepository : ICensusRepository
    {
        private readonly CensusDbContext censusDbContext;

        public CensusRepository(CensusDbContext censusDbContext)
        {
            this.censusDbContext = censusDbContext;
        }

        public async Task<List<CensusEntity>> GetActualCensusEntitiesByStateIdsAsync(int[] stateIds)
        {
            var censusEntities = await censusDbContext.ActualCensusEntities
                                        .Where(e => stateIds.Contains(e.StateId))
                                        .Select(e => new CensusEntity
                                        {
                                            StateId = e.StateId,
                                            Households = e.Households,
                                            Population = e.Population
                                        })
                                        .ToListAsync();

            return censusEntities;
        }

        public async Task<List<CensusEntity>> GetEstimatedCensusEnitiesByStateIdsAsync(int[] stateIds)
        {
            var censusEntities = await censusDbContext.EstimatedCensusEntities
                                        .Where(e => stateIds.Contains(e.StateId))
                                        .GroupBy(e => e.StateId)
                                        .Select(g => new CensusEntity
                                        {
                                            StateId = g.Key,
                                            Households = g.Sum(e => e.Households),
                                            Population = g.Sum(e => e.Population)
                                        })
                                        .ToListAsync();

            return censusEntities;
        }
    }
}
