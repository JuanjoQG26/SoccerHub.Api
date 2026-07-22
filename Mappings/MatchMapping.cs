using SoccerHub.Api.DTOs.Match;
using SoccerHub.Api.Models;

namespace SoccerHub.Api.Mappings
{
    public static class MatchMapping
    {
        public static MatchDto ToDto(this Match match)
        {
            return new MatchDto
            {
                Id = match.Id,
                HomeTeam = match.HomeTeam.Name,
                AwayTeam = match.AwayTeam.Name,
                MatchDate = match.Matchdate,
                Stadium = match.Stadium,
                HomeGoals = match.HomeGoals,
                AwayGoals = match.AwayGoals,
                Status = match.Status.ToString()
            };
        }
    }
}
