using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.ServiceLayer.Dtos
{
  public  class EventsListDto
    {
        public List<EventDto> allEvents { get; set; }
        public EventDto LatestEvent { get; set; }

    }
}
