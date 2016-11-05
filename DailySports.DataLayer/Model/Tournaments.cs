using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DailySports.DataLayer.Model
{
   public class Tournaments
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        public string Format { get; set; }
        [Required]
        public string Overview { get; set; }
        [Required]
        public string MainEvent { get; set; }
        [Required]
        public string Qualifiers { get; set; }
        [Required]
        [AllowHtml]
        public string Description { get; set; }
        [AllowHtml]

        public string URL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public List<string> Stream { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
        public virtual ICollection<Videos> Videos { get; set; }
        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }
        public int GameId { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<GroupStages> GroupStages { get; set; }
        public virtual ICollection<PrizePool> PrizePool { get; set; }
        public string TournamentImage { get; set; }
    }
}
