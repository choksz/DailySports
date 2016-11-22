using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailySports.DataLayer.Model
{
    public class PrizePool
    {
        [Key]
        public int Id { get; set; }
        public virtual ICollection<PlaceEntry> Distribution { get; set; }

        [ForeignKey("TournamentId")]
        public virtual Tournaments Tournament { get; set; }
        public int TournamentId { get; set; }
    }
}
