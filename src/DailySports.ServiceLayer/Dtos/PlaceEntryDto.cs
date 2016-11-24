using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public class PlaceEntryDto
    {
        public int Id { get; set; }
        public int Place { get; set; }
        public string Amount { get; set; }

        public int PrizePoolId { get; set; }

        public int TeamId { get; set; }
        public TeamDto Team { get; set; }

        public PlaceEntryDto() { }
        public PlaceEntryDto(PlaceEntry e)
        {
            Id = e.Id;
            Place = e.Place;
            Amount = e.Amount;
            PrizePoolId = e.PrizePoolId;
            Team = (e.Team != null) ? new TeamDto(e.Team) : null;
        }
    }
}
