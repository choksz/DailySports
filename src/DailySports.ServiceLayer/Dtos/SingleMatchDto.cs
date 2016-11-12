using System.Collections.Generic;

namespace DailySports.ServiceLayer.Dtos
{
    public class SingleMatchDto
    {
       public MatchDto Match { get; set; }
        public PetOfTheWeekDto Pet { get; set; }
        public List<MatchDto> TournamentMatches { get; set; }
    }
}
