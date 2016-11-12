using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailySports.DataLayer.Model
{
    public  class VideoComments
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("CommentId")]
        public virtual Comments Comment { get; set; }
        public int CommentId { get; set; }
        [ForeignKey("VideoId")]
        public virtual Videos Video { get; set; }
        public int VideoId { get; set; }
    }
}
