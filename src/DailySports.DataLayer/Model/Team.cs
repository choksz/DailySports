using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailySports.DataLayer.Model
{
    public  class Team
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<Player> Players { get; set; }

        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
        public int GameId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
        public int CountryCode { get; set; }
    }
}
