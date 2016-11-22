using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailySports.DataLayer.Model
{
    public class PlaceEntry
    {
        [Key]
        public int Id { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public double Amount { get; set; }

        [ForeignKey("PrizePoolId")]
        public virtual PrizePool PrizePool { get; set; }
        public int PrizePoolId { get; set; }
    }
}
