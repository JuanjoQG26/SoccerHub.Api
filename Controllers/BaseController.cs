using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoccerHub.Api.DTOs;
using System.Security.Claims;

namespace SoccerHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected int CurrentUserId
        {
            get
            {
                var claim = User.FindFirst(ClaimTypes.NameIdentifier);

                if (claim == null)
                {
                    throw new UnauthorizedAccessException();
                }

                return int.Parse(claim.Value);
            }
        }

        protected string CurrentUserRole
        {
            get
            {
                return User.FindFirst(ClaimTypes.Role)?.Value ?? "";
            }
        }

        protected string CurrentUserEmail
        {
            get
            {
                return User.FindFirst(ClaimTypes.Email)?.Value ?? "";
            }
        }
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
