using System.Collections.Generic;
using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public class PrizePoolDto
    {
        public int Id { get; set; }
        public List<PlaceEntryDto> Distribution { get; set; }
        
        public PrizePoolDto() { }
        public PrizePoolDto(PrizePool prizePool)
        {
            Id = prizePool.Id;

            Distribution = new List<PlaceEntryDto>();
            if (prizePool.Distribution != null)
            {
                foreach (PlaceEntry e in prizePool.Distribution)
                    Distribution.Add(new PlaceEntryDto(e));
            }
        }
    }
}
