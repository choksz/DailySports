using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Web.Mvc;

namespace DailySports.DataLayer.Model
{
    public  class Videos
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]    
        [AllowHtml]
        public string Description { get; set; }

        [Required]
        [AllowHtml]

        public string Url { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public string Tag { get; set; }

        [ForeignKey("GameId")]
        public virtual Game Game { get; set; }

        [Required]
        public int GameId { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("TournamentId")]
        public virtual Tournaments Tournament { get; set; }
        public int TournamentId { get; set; }
       
    }
}
