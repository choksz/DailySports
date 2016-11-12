using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailySports.DataLayer.Model
{
    public class News
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        //[AllowHtml]
        public string Description { get; set; }
        public string NewsImage { get; set; }
        public DateTime Date { get; set; }
        public string Tag { get; set; }

        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category category { get; set; }

        public int GameId { get; set; }
        [ForeignKey ("GameId")]
        public virtual Game game { get; set; }
        
        public Status status { get; set; }

        public int TournamentId { get; set; }
        [ForeignKey("TournamentId")]
        public virtual Tournaments Tournament { get; set; }
    }
}
