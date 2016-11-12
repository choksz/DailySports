using System.ComponentModel.DataAnnotations;

namespace DailySports.DataLayer.Model
{
    public class Privilge
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
