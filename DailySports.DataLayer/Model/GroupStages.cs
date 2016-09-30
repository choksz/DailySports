using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.DataLayer.Model
{
   public class GroupStages
    {
        [Key]
        public int Id { get; set; }
        public virtual ICollection<Team> Team { get; set; }
        [ForeignKey("TournamentId")]
        public virtual Tournaments Tournament { get; set; }
        public int TournamentId { get; set; }
    }
}
