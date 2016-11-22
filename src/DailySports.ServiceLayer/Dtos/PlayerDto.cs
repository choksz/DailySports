using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nick { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
        public string Notes { get; set; }

        public int TeamId { get; set; }
        public TeamDto Team { get; set; }

        public int CountryCode { get; set; }
        public CountryDto Country { get; set; }

        public PlayerDto() { }
        public PlayerDto(Player player)
        {
            Id = player.Id;
            Name = player.Name;
            Nick = player.Nick;
            Age = player.Age;
            Role = player.Role;
            Notes = player.Notes;
            TeamId = player.TeamId;
            Team = (player.Team != null) ? new TeamDto(player.Team) : null;
            CountryCode = player.CountryCode;
            Country = (player.Country != null) ? new CountryDto(player.Country) : null;
        }
    }
}
