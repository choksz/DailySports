using System.Collections.Generic;

namespace DailySports.ServiceLayer.Dtos
{
    public  class TounamentListDto
    {
        public List<TournementsDto> AllTournaments { get; set; }
        public List<TournementsDto> LatestTournament { get; set; }
        public List<GameDto> AllGames { get; set; }
    }
}
