using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailySports.DataLayer.Model
{
    /*
        Used for modelling Many-To-Many Relationship
    */
    public class TeamListTeam
    {
        [ForeignKey("TeamListId")]
        public virtual TeamList TeamList { get; set; }
        public int TeamListId { get; set; }

        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }
        public int TeamId { get; set; }
    }
}
