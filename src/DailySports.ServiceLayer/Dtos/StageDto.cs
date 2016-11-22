using System.Collections.Generic;
using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public class StageDto
    {
        public int Id { get; set; }
        public List<TeamDto> TeamList { get; set; }
        
        public StageDto() { }
        public StageDto(Stage gs)
        {
            Id = gs.Id;
            TeamList = new List<TeamDto>();
        }
    }
}
