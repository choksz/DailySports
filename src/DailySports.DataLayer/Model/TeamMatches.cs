using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailySports.DataLayer.Model
{
    public class TeamMatches
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("MatchId")]
        public virtual Match Match { get; set; }
        public int MatchId { get; set; }
        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }
        public int TeamId { get; set; }

    }
}
