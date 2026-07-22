using Microsoft.EntityFrameworkCore;
using SoccerHub.Api.Data;
using SoccerHub.Api.DTOs.Match;
using SoccerHub.Api.Models;
using SoccerHub.Api.Mappings;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using SoccerHub.Api.DTOs.Common;

namespace SoccerHub.Api.Services
{
    public class MatchService
    {
        private readonly AppDbContext _context;

        public MatchService(AppDbContext context) {
            _context = context;
        }

        public async Task<MatchDto> CreateAsync(CreateMatchDto dto)
        {
            if (dto.HomeTeamId == dto.AwayTeamId)
            {
                throw new Exception("A team cannot play against itself");
            }

            var homeTeam = await _context.Teams.FirstOrDefaultAsync(t => t.Id == dto.HomeTeamId);

            if (homeTeam == null)
            {
                throw new Exception("Home team not found");
            }

            var awayTeam = await _context.Teams.FirstOrDefaultAsync(t => t.Id == dto.AwayTeamId);

            if (awayTeam == null)
            {
                throw new Exception("Away team not found");
            }

            var match = new Match
            {
                HomeTeamId = dto.HomeTeamId,
                AwayTeamId = dto.AwayTeamId,
                Matchdate = dto.MatchDate,
                Stadium = dto.Stadium,
                HomeGoals = 0,
                AwayGoals = 0,
                Status = MatchStatus.Schelduled
            };

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            /*return new MatchDto
            {
                Id = match.Id,
                HomeTeam = homeTeam.Name,
                AwayTeam = awayTeam.Name,
                MatchDate = match.Matchdate,
                Stadium = match.Stadium,
                HomeGoals = match.HomeGoals,
                AwayGoals = match.AwayGoals,
                Status = match.Status.ToString()
            };*/
            return match.ToDto();
        }

        public async Task<PagedResponseDto<MatchDto>> GetAllAsync(MatchFilterDto filter)
        {
            /*return await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Select(m => new MatchDto
                {
                    Id = m.Id,
                    HomeTeam = m.HomeTeam.Name,
                    AwayTeam = m.AwayTeam.Name,
                    MatchDate = m.Matchdate,
                    Stadium = m.Stadium,
                    HomeGoals = m.HomeGoals,
                    AwayGoals = m.AwayGoals,
                    Status = m.Status.ToString()
                })
                .ToListAsync();*/

            var query = _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .AsQueryable();

            if (filter.TeamId.HasValue)
            {
                query = query.Where(m =>
                    m.HomeTeamId == filter.TeamId.Value ||
                    m.AwayTeamId == filter.TeamId.Value);
            }


            if (filter.Status.HasValue)
            {
                query = query.Where(m =>
                    m.Status == filter.Status.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.Search))
            {
                query = query.Where(m =>
                    m.HomeTeam.Name.Contains(filter.Search) ||
                    m.AwayTeam.Name.Contains(filter.Search));
            }

            if (filter.Date.HasValue)
            {
                query = query.Where(m =>
                    m.Matchdate.Date == filter.Date.Value.Date);
            }

            switch (filter.SortBy?.ToLower())
            {
                case "date":
                    query = query.OrderBy(m => m.Matchdate);
                    break;
                case "date_desc":
                    query = query.OrderByDescending(m => m.Matchdate);
                    break;
                default:
                    query = query.OrderBy(m => m.Id);
                    break;
            }

            var totalItems = await query.CountAsync();

            var matches = await query.Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return new PagedResponseDto<MatchDto>
            {
                Items = matches.Select(x => x.ToDto()).ToList(),

                Page = filter.Page,
                PageSize = filter.PageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling((double)totalItems / filter.PageSize)
            };
        }

        public async Task<List<MatchDto>> GetSchelduleAsync()
        {
            var matches = await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Where(m => m.Status == MatchStatus.Schelduled)
                .OrderBy(m => m.Matchdate)
                .ToListAsync();

            return matches.Select(m => m.ToDto()).ToList();
        }

        public async Task<List<MatchDto>> GetByTeamAsync(int teamId)
        {
            var matches = await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId)
                .OrderByDescending(m => m.Matchdate)
                .ToListAsync();

            return matches.Select(m => m.ToDto()).ToList();
        }

        public async Task<PagedResponseDto<MatchDto>> GetPagedAsync(PaginationDto pagination)
        {
            var totalItems = await _context.Matches.CountAsync();

            var matches = await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .OrderBy(m => m.Matchdate)
                .Skip((pagination.Page - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync();

            return new PagedResponseDto<MatchDto>
            {
                Items = matches.Select(m => m.ToDto()).ToList(),
                Page = pagination.Page,
                PageSize = pagination.PageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling((double)totalItems / pagination.PageSize)
            };
        }
    }
}
