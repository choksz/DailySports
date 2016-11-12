using System.Collections.Generic;
using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public  class TeamDto
    {
        public int Id { get; set;}
        public string Name { get; set; }
        public List<PlayerDto> Player { get; set; }

        public TeamDto() { }
        public TeamDto(Team team)
        {
            Id = team.Id;
            Name = team.Name;
            Player = new List<PlayerDto>();
            foreach (Player p in team.Players) {
                Player.Add(new PlayerDto(p));
            }
        }
    }
}
