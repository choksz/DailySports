using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.DataLayer.Model
{
  public  class NewsComments
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("CommentId")]
        public virtual Comments Comment { get; set; }
        public int CommentId { get; set; }
        [ForeignKey("NewsId")]
        public virtual News News { get; set; }
        public int NewsId { get; set; }
    }
}
