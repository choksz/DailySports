using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailySports.DataLayer.Model
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        [ForeignKey("TeamId")]
        public virtual Team team { get; set; }
        public int TeamId { get; set; }
    }
}
