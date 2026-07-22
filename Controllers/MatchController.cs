using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoccerHub.Api.DTOs;
using SoccerHub.Api.DTOs.Common;
using SoccerHub.Api.DTOs.Match;
using SoccerHub.Api.Helpers;
using SoccerHub.Api.Services;

namespace SoccerHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MatchController : BaseController
    {
        private readonly MatchService _matchService;

        public MatchController(MatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMatchDto dto)
        {
            var match = await _matchService.CreateAsync(dto);

            return Ok(ApiResponseHelper.Success(match, "Match created successfully"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] MatchFilterDto filter)
        {
            var matches = await _matchService.GetAllAsync(filter);

            return Ok(ApiResponseHelper.Success(matches));
        }

        [HttpGet("schelduled")]
        public async Task<IActionResult> GetShelduled()
        {
            var matches = await _matchService.GetSchelduleAsync();

            return Ok(ApiResponseHelper.Success(matches));
        }

        [HttpGet("team/{teamId}")]
        public async Task<IActionResult> GetByTeam(int teamId)
        {
            var matches = await _matchService.GetByTeamAsync(teamId);

            return Ok(ApiResponseHelper.Success(matches));
        }

        /*[HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] PaginationDto pagination)
        {
            var result = await _matchService.GetPagedAsync(pagination.Page, pagination.PageSize);

            return Ok(ApiResponseHelper.Success(result));
        }*/

        /*[HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] MatchFilterDto filter)
        {
            var result = await _matchService.GetAllAsync(filter);

            return Ok(ApiResponseHelper.Success(result));
        }*/
    }
}
