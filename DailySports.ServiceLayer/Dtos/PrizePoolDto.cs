using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.ServiceLayer.Dtos
{
   public class PrizePoolDto
    {
        public int Id { get; set; }
        public int Prize { get; set; }
        public int Level { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }
}
