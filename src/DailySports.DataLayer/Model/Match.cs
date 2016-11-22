using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailySports.DataLayer.Model
{
    public class Match
    {
        [Key]
        public int Id { get; set; }
        public int ScoreA { get; set; }
        public int ScoreB { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("StageId")]
        public virtual Stage Stage { get; set; }
        public int StageId { get; set; }

        [ForeignKey("TeamAId")]
        public virtual Team TeamA { get; set; }
        public int TeamAId { get; set; }

        [ForeignKey("TeamBId")]
        public virtual Team TeamB { get; set; }
        public int TeamBId { get; set; }
    }
}
