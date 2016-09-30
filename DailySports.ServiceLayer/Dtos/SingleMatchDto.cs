using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.ServiceLayer.Dtos
{
   public class SingleMatchDto
    {
       public MatchDto Match { get; set; }
        public PetOfTheWeekDto Pet { get; set; }
        public List<MatchDto> TournamentMatches { get; set; }
    }
}
