using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.ServiceLayer.Dtos
{
  public class EventImageDto
    {
        public int Id { get; set; }
        public string Tag { get; set; }
        public string File { get; set; }
        public int EventId { get; set; }

    }
}
