using DailySports.DataLayer.Model;
using System.Collections.Generic;

namespace DailySports.ServiceLayer.Dtos
{
    public class TeamListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<TeamDto> Teams { get; set; }

        public TeamListDto() { }
        public TeamListDto(TeamList tl)
        {
            Id = tl.Id;
            Name = tl.Name;
            Description = tl.Description;
            Teams = new List<TeamDto>();
            if (tl.Teams != null)
            {
                foreach (var team in tl.Teams)
                {
                    Teams.Add(new TeamDto(team));
                }
            }
        }
    }
}
