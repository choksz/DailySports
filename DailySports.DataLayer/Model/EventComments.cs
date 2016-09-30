using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.DataLayer.Model
{
 public   class EventComments
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("CommentId")]
        public virtual Comments Comments { get; set; }
        public int CommentId { get; set; }
        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }
        public int EventId { get; set; }
    }
}
