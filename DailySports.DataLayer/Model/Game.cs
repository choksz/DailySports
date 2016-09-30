using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.DataLayer.Model
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string GameImage { get; set; }
        public virtual ICollection<Videos> Videos { get; set; }
        public string LiveStreamUrl { get; set; }
    }
}
