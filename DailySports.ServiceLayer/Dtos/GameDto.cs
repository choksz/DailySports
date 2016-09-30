using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.ServiceLayer.Dtos
{
   public class GameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GameImage { get; set; }
        public string LiveStreamURL { get; set; }
    }
}
