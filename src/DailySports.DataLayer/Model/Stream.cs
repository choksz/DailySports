using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DailySports.DataLayer.Model
{
    public class Stream
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public bool Main { get; set; }

        [ForeignKey("LanguageCode")]
        public virtual Language Language { get; set; }
        public string LanguageCode { get; set; }
        
        [ForeignKey("TournamentId")]
        public virtual Tournaments Tournament { get; set; }
        public int TournamentId { get; set; }
    }
}
