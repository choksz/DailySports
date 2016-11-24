using System.Collections.Generic;
using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public class PrizePoolDto
    {
        public int Id { get; set; }
        public string Amount { get; set; }
        public List<PlaceEntryDto> Distribution { get; set; }
        
        public PrizePoolDto() { }
        public PrizePoolDto(PrizePool prizePool)
        {
            Id = prizePool.Id;
            Amount = prizePool.Amount;
            Distribution = new List<PlaceEntryDto>();
            if (prizePool.Distribution != null)
            {
                foreach (var e in prizePool.Distribution)
                {
                    Distribution.Add(new PlaceEntryDto(e));
                }
            }
        }
    }
}
