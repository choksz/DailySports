using System;
using System.Collections.Generic;

namespace DailySports.ServiceLayer.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string EventImage { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
        public int Tickets { get; set; }
        public GameDto Game { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        public List<EventImageDto> EventImages { get; set; }
        public PetOfTheWeekDto PetOfTheWeekDto { get; set; }
        public List<MatchDto> NextMatches { get; set; }
    }
}
