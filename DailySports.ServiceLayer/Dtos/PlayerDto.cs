using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.ServiceLayer.Dtos
{
   public class PlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }
}
