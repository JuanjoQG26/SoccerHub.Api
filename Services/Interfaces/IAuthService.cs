using SoccerHub.Api.DTOs;

namespace SoccerHub.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDTO dto);

        Task<AuthResponseDto> LoginAsync(LoginDto dto);
    }
}
