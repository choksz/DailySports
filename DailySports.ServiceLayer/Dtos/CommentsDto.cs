using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.ServiceLayer.Dtos
{
   public class CommentsDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int NewsId { get; set; }
    }
}
