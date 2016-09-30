using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.ServiceLayer.Dtos
{
  public  class VideoDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
      
        public string URL { get; set; }
        public int GameId { get; set; }
        public List<PetOfTheWeekDto> petOfTheDay { get; set; }
        public List<MatchDto> NextMatches { get; set; }
        public int TournamentID { get; set; }
        
    }
}
