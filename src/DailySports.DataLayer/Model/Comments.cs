using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailySports.DataLayer.Model
{
    public  class Comments
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual User user { get; set; } 
    }
}
