using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoccerHub.Api.Helpers;
using SoccerHub.Api.Services;

namespace SoccerHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : BaseController
    {
        private readonly DashboardService _dashboardService;

        public DashboardController(DashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dashboard = await _dashboardService.GetAsync();

            return Ok(ApiResponseHelper.Success(dashboard));
        }
    }
}
