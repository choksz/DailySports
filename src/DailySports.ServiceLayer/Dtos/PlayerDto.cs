using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        //public TeamDto Team { get; set; }

        public PlayerDto() { }
        public PlayerDto(Player player)
        {
            Id = player.Id;
            Name = player.Name;
            TeamId = player.team.Id;
            TeamName = player.team.Name;
            //Team = new TeamDto(player.team);
        }
    }
}
