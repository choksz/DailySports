using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public class PlaceEntryDto
    {
        public int Id { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public double Amount { get; set; }

        public int PrizePoolId { get; set; }
        public PrizePoolDto PrizePool { get; set; }

        public PlaceEntryDto() { }
        public PlaceEntryDto(PlaceEntry e)
        {
            Id = e.Id;
            From = e.From;
            To = e.To;
            Amount = e.Amount;
            PrizePoolId = e.PrizePoolId;
            PrizePool = (e.PrizePool != null) ? new PrizePoolDto(e.PrizePool) : null;
        }
    }
}
