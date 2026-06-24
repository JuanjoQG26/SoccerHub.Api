using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoccerHub.Api.DTOs;

namespace SoccerHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult Success<T>(T data, string message)
        {
            return Ok(new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            });
        }
    }
}
