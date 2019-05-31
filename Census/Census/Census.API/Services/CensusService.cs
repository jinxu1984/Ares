using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Census.API.Dto;
using Census.API.Infrastructure.Pagination;
using Census.API.Model;
using Census.API.Repositories;
using Census.API.Extensions;

namespace Census.API.Services
{
    public class CensusService : ICensusService
    {
        private readonly ICensusRepository censusRepository;

        public CensusService(ICensusRepository censusRepository)
        {
            this.censusRepository = censusRepository;
        }

        public async Task<List<CensusEntity>> GetCensusEntitiesByStateIdsAsync(int[] stateIds)
        {
            var censusEnties = new List<CensusEntity>();

            var actualCensusEntities = await censusRepository.GetActualCensusEntitiesByStateIdsAsync(stateIds);
            censusEnties.AddRange(actualCensusEntities);

            var stateIdsHaveFoundCensusEntities = actualCensusEntities.Select(e => e.StateId);
            var stateIdsHaveNotFoundCensusEntities = stateIds.Where(i => !stateIdsHaveFoundCensusEntities.Contains(i)).ToArray();

            if (stateIdsHaveNotFoundCensusEntities.Count() > 0)
            {
                var estimatedCensusEntities = await censusRepository.GetEstimatedCensusEnitiesByStateIdsAsync(stateIdsHaveNotFoundCensusEntities);
                censusEnties.AddRange(estimatedCensusEntities);
            }

            return censusEnties;
        }
    }
}
