using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.DataLayer.Model
{
   public class Match
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("TournamentId")]
        public virtual Tournaments Tournament { get; set; }
        public int TournamentId { get; set; }
        [ForeignKey("TeamAId")]
        public virtual Team TeamA { get; set; }
        public int TeamAId { get; set; }
        [ForeignKey("TeamBId")]
        public virtual Team TeamB { get; set; }
        public int TeamBId { get; set; }
    }
}
