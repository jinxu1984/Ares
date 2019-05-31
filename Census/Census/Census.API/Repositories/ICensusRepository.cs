using Census.API.Infrastructure.Pagination;
using Census.API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Census.API.Repositories
{
    public interface ICensusRepository
    {
        Task<List<CensusEntity>> GetActualCensusEntitiesByStateIdsAsync(int[] stateIds);
        Task<List<CensusEntity>> GetEstimatedCensusEnitiesByStateIdsAsync(int[] stateIds);
    }
}
