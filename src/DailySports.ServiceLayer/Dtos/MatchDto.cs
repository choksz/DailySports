using System;
using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public  class MatchDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TeamAId { get; set; }
        public string TeamAName { get; set; }
        public int TeamBId { get; set; }
        public string TeamBName { get; set; }
        public int TournamentId { get; set; }
        public string TournamentName { get; set; }
        public TeamDto TeamA { get; set; }
        public TeamDto TeamB { get; set; }
        public TournementsDto Tournament { get; set; }

        public MatchDto() { }
        public MatchDto(Match match)
        {
            Id = match.Id;
            Date = match.Date;
            TeamAId = match.TeamAId;
            TeamAName = match.TeamA.Name;
            TeamBId = match.TeamBId;
            TeamBName = match.TeamB.Name;
            TournamentId = match.TournamentId;
            TournamentName = match.Tournament.Title;
            TeamA = new TeamDto(match.TeamA);
            TeamB = new TeamDto(match.TeamB);
        }
    }
}
