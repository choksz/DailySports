using System.ComponentModel.DataAnnotations;

namespace DailySports.DataLayer.Model
{
    public class Language
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }
    }
}
