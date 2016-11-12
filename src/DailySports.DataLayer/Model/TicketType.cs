using System.ComponentModel.DataAnnotations;

namespace DailySports.DataLayer.Model
{
    public class TicketType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
    }
}
