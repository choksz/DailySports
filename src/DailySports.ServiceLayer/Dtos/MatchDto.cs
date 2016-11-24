using System;
using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public  class MatchDto
    {
        public int Id { get; set; }
        public int ScoreA { get; set; }
        public int ScoreB { get; set; }
        public DateTime Date { get; set; }

        public int StageId { get; set; }
        public StageDto Stage { get; set; }

        public int TeamAId { get; set; }
        public TeamDto TeamA { get; set; }

        public int TeamBId { get; set; }
        public TeamDto TeamB { get; set; }

        public MatchDto() { }
        public MatchDto(Match match)
        {
            Id = match.Id;
            ScoreA = match.ScoreA;
            ScoreB = match.ScoreB;
            Date = match.Date;

            StageId = match.StageId;

            TeamAId = match.TeamAId;
            TeamA = (match.TeamA != null) ? new TeamDto(match.TeamA) : null;

            TeamBId = match.TeamBId;
            TeamB = (match.TeamB != null) ? new TeamDto(match.TeamB) : null;
            
        }
    }
}
