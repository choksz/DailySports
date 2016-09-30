using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.ServiceLayer.Dtos
{
  public  class MatchDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TeamAId { get; set; }
        public string TeamAName { get; set; }
        public int TeamBId { get; set; }
        public string TeamBName { get; set; }
        public int TournamentId { get; set; }
        public string TournamentName { get; set; }

    }
}
