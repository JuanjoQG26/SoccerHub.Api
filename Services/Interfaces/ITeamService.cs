using SoccerHub.Api.DTOs;
using SoccerHub.Api.DTOs.Common;

namespace SoccerHub.Api.Services.Interfaces
{
    public interface ITeamService
    {
        Task<TeamDto> CreateAsync(CrearTeamDto dto, int userId);

        Task<List<TeamDto>> GetMyTeamsAsync(int userId);

        Task<TeamDetailsDto> GetByIdAsync(int teamId, int userId);

        Task<bool> UpdateAsync(int id, UpdateTeamDto dto, int userId);

        Task<bool> DeleteAsync(int id, int userId);

        /*Task<PagedResponseDto<TeamDto>> GetPagedAsync(PaginationDto pagination);*/
    }
}
