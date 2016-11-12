using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailySports.DataLayer.Model
{
    public class PrizePool
    {
        [Key]
        public int Id { get; set; }
        public int Prize { get; set; }
        public int Level { get; set; }
        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }
        public int TeamId { get; set; }
        [ForeignKey("TournamentId")]
        public virtual Tournaments Tournament { get; set; }
        public int TournamentId { get; set; }

    }
}
