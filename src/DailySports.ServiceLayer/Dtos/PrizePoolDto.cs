using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public class PrizePoolDto
    {
        public int Id { get; set; }
        public int Prize { get; set; }
        public int Level { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public TeamDto Team { get; set; }

        public PrizePoolDto() { }
        public PrizePoolDto(PrizePool prizePool)
        {
            Id = prizePool.Id;
            Prize = prizePool.Prize;
            Level = prizePool.Level;
            TeamId = prizePool.TeamId;
            TeamName = prizePool.Team.Name;
            Team = new TeamDto(prizePool.Team);
        }
    }
}
