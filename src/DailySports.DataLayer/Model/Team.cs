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
        [ForeignKey("GroupStageId")]
        public virtual GroupStages GroupStage { get; set; }
        public int GroupStageId { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}
