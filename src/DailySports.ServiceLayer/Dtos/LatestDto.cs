using System.Collections.Generic;

namespace DailySports.ServiceLayer.Dtos
{
    public class LatestDto
    {
        public List<EventDto> LatestEvents { get; set; }
        public List<NewsDto> LatestNews { get; set; }
        public List<TournementsDto> LatestTournament { get; set; }
        public List<VideoDto> LatestVideos { get; set; }
        public List<MatchDto> NextMatches { get; set; }
        public List<PetOfTheWeekDto> PetOfTheWeek { get; set; }
        public List<GameDto> LiveGames { get; set; }
        public List<TournementsDto> OngoingTournaments { get; set; }
        public List<CarouselItemDto> CarouselItems { get; set; }
    }
}
