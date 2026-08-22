using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoccerHub.Api.Helpers;
using SoccerHub.Api.Services;

namespace SoccerHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StandingsController : BaseController
    {
        private readonly StandingService _standingService;

        public StandingsController(StandingService standingService)
        {
            _standingService = standingService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var standings = await _standingService.GetAsync();

            return Ok(ApiResponseHelper.Success(standings));
        }
    }
}
