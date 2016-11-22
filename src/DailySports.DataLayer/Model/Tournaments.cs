using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailySports.DataLayer.Model
{
    public class Tournaments
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        public string Overview { get; set; }
        public string URL { get; set; }
        public string Venue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TournamentImage { get; set; }

        public virtual PrizePool PrizePool { get; set; }

        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
        public int GameId { get; set; }

        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<Stage> Stages { get; set; }
        public virtual ICollection<Stream> Streams { get; set; }
    }
}
