using System.Collections.Generic;
using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public class TeamListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<TeamDto> Teams { get; set; }

        public int TournamentId { get; set; }

        public TeamListDto() { }
        public TeamListDto(TeamList l)
        {
            Id = l.Id;
            Name = l.Name;
            Description = l.Description;
            Teams = new List<TeamDto>();
            if (l.Teams != null)
            {
                foreach (var t in l.Teams)
                {
                    Teams.Add(new TeamDto(t));
                }
            }
            TournamentId = l.TournamentId;
        }
    }
}
