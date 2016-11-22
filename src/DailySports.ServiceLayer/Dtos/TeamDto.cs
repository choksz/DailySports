using System.Collections.Generic;
using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public  class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }

        public List<PlayerDto> Players { get; set; }

        public int GameId { get; set; }
        public GameDto Game { get; set; }

        public string CountryCode { get; set; }
        public CountryDto Country { get; set; }
        
        public TeamDto() { }
        public TeamDto(Team team)
        {
            Id = team.Id;
            Name = team.Name;
            Logo = team.Logo;

            Players = new List<PlayerDto>();
            if (team.Players != null)
            foreach (Player p in team.Players) {
                Players.Add(new PlayerDto(p));
            }

            GameId = team.GameId;
            Game = (team.Game != null) ? new GameDto(team.Game) : null;

            CountryCode = team.CountryCode;
            Country = (team.Country != null) ? new CountryDto(team.Country) : null;
        }
    }
}
