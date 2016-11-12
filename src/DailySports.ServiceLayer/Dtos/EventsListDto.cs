using System.Collections.Generic;

namespace DailySports.ServiceLayer.Dtos
{
    public  class EventsListDto
    {
        public List<EventDto> allEvents { get; set; }
        public EventDto LatestEvent { get; set; }

    }
}
