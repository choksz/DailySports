using System.Collections.Generic;
using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public class GroupStagesDto
    {
        public int Id { get; set; }
        public List<TeamDto> TeamList { get; set; }
        
        public GroupStagesDto() { }
        public GroupStagesDto(GroupStages gs)
        {
            Id = gs.Id;
            TeamList = new List<TeamDto>();
            foreach (var team in gs.Team)
            {
                TeamList.Add(new TeamDto(team));
            }
        }
    }
}
