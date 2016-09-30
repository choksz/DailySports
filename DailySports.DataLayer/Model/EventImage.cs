using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.DataLayer.Model
{
    public class EventImage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Tag { get; set; }
        public string File { get; set; }
        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }
        [Required]
        public int EventId { get; set; }
    }
}
