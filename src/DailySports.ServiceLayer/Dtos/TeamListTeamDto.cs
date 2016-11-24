using System.Collections.Generic;

namespace DailySports.ServiceLayer.Dtos
{
    public class TeamListTeamDto
    {        
        public int TeamId { get; set; }
        public TeamDto Team { get; set; }

        public int TournamentId { get; set; }
        public TournementsDto Tournament { get; set; }
    }
}
