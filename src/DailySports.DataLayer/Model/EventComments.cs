using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
