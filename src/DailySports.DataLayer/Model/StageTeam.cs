using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailySports.DataLayer.Model
{
    /*
        Used for modelling Many-To-Many Relationship
    */
    public class StageTeam
    {
        [ForeignKey("StageId")]
        public virtual Stage Stage { get; set; }
        public int StageId { get; set; }

        [ForeignKey("TeamId")]
        public virtual Team Team { get; set; }
        public int TeamId { get; set; }
    }
}
