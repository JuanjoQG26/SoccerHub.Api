using SoccerHub.Api.DTOs.Common;
using SoccerHub.Api.Models;

namespace SoccerHub.Api.DTOs.Match
{
    public class MatchFilterDto : PaginationDto
    {
        public int? TeamId { get; set; }

        public MatchStatus? Status { get; set; }

        public string? Search {  get; set; }

        public DateTime? Date {  get; set; }

        public string? SortBy { get; set; }
    }
}
