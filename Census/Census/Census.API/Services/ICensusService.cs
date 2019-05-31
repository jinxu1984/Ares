using Census.API.Dto;
using Census.API.Infrastructure.Pagination;
using Census.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Census.API.Services
{
    public interface ICensusService
    {
        Task<List<CensusEntity>> GetCensusEntitiesByStateIdsAsync(int[] stateIds);
    }
}
