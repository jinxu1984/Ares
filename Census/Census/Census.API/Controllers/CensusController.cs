using Census.API.Dto;
using Census.API.Extensions;
using Census.API.Infrastructure.Pagination;
using Census.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Census.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CensusController : ControllerBase
    {
        private readonly ICensusService censusService;

        public CensusController(ICensusService censusService)
        {
            this.censusService = censusService;
        }

        [HttpGet("households")]
        [ProducesResponseType(typeof(ActionResult<PagedList<HouseholdDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<PagedList<HouseholdDto>>> GetHouseholdsByStatesAsync(
            [FromQuery(Name ="state"), Required] int[] stateIds, 
            [FromQuery, Range(1, int.MaxValue)]int pageIndex = 1,
            [FromQuery, Range(1, int.MaxValue)]int pageSize = 10)
        {
            stateIds = stateIds.Distinct().ToArray();
            var censusEntities = await censusService.GetCensusEntitiesByStateIdsAsync(stateIds);

            if (censusEntities.Count != stateIds.Count()) { return NotFound(); }

            var pagedHouseholdsDtos = censusEntities.ToPageDtos(
                (e) => new HouseholdDto { State = e.StateId, HouseHolds = e.Households },
                pageIndex, pageSize);

            return Ok(pagedHouseholdsDtos);
        }

        [HttpGet("population")]
        [ProducesResponseType(typeof(ActionResult<PagedList<HouseholdDto>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<PagedList<PopulationDto>>> GetPopulationByStatesAsync(
            [FromQuery(Name = "state"), Required] int[] stateIds,
            [FromQuery, Range(1, int.MaxValue)]int pageIndex = 1,
            [FromQuery, Range(1, int.MaxValue)]int pageSize = 10)
        {
            stateIds = stateIds.Distinct().ToArray();
            var censusEntities = await censusService.GetCensusEntitiesByStateIdsAsync(stateIds);

            if (censusEntities.Count != stateIds.Count()) { return NotFound(); }

            var pagedPopulationDtos = censusEntities.ToPageDtos(
                (e) => new PopulationDto { State = e.StateId, Population = e.Population },
                pageIndex, pageSize);

            return Ok(pagedPopulationDtos);
        }
    }
}