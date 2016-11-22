using System.ComponentModel.DataAnnotations;

namespace DailySports.DataLayer.Model
{

    public class Country
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }
    }
}
