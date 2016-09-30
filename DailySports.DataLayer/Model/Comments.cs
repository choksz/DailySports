using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.DataLayer.Model
{
   public  class Comments
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        [ForeignKey("UserId")]
        public virtual User user { get; set; }
        public int UserId { get; set; }
      
       
        
    }
}
