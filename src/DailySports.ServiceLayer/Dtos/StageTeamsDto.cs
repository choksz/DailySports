using System.Collections.Generic;
using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public class StageTeamsDto
    {        
        public List<TeamDto> Teams { get; set; }

        public int StageId { get; set; }
        public StageDto Stage { get; set; }

        public StageTeamsDto() { }
        public StageTeamsDto(ICollection<StageTeam> l)
        {
            Teams = new List<TeamDto>();
            foreach (StageTeam st in l) {
                StageId = st.StageId;
                Stage = new StageDto(st.Stage);
                Teams.Add(new TeamDto(st.Team));
            }
        }
    }
}
