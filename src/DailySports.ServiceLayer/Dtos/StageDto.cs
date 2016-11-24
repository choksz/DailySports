using System.Collections.Generic;
using DailySports.DataLayer.Model;
using System;

namespace DailySports.ServiceLayer.Dtos
{
    public class StageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        public List<MatchDto> Matches { get; set; }

        public StageDto() { }
        public StageDto(Stage gs)
        {
            Id = gs.Id;
            Name = gs.Name;
            Description = gs.Description;
            StartDate = gs.StartDate;
            EndDate = gs.EndDate;
            Matches = new List<MatchDto>();
            if (gs.Matches != null)
            {
                foreach (var match in gs.Matches)
                {
                    Matches.Add(new MatchDto(match));
                }
            }
        }
    }
}
