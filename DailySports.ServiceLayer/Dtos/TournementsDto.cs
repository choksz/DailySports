using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Entity;
namespace DailySports.ServiceLayer.Dtos
{
   public class TournementsDto : DbContext
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Format { get; set; }
        public string Overview { get; set; }
        public string MainEvent { get; set; }
        public string Qualifiers { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public int GameId { get; set; }
        public string TournamentImage { get; set; }
        public List<MatchDto> TournamentMatches { get; set; }
        public List<PrizePoolDto> TournamentPrizePool { get; set; }
        public List<GroupStagesDto> TournamentGroupStages { get; set; }
        public List<MatchDto> NextMatches { get; set; }
    }
}
