using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailySports.DataLayer.Model
{
    public class PlaceEntry
    {
        [Key]
        public int Id { get; set; }
        public int Place { get; set; }
        public string Amount { get; set; }

        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }
        public int TeamId { get; set; }

        [ForeignKey("PrizePoolId")]
        public virtual PrizePool PrizePool { get; set; }
        public int PrizePoolId { get; set; }
    }
}
