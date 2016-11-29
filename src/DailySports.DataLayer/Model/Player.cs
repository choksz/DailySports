using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailySports.DataLayer.Model
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nick { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
        public string Notes { get; set; }

        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }
        public int TeamId { get; set; }

        [ForeignKey("CountryCode")]
        public virtual Country Country { get; set; }
        public string CountryCode { get; set; }
    }
}
