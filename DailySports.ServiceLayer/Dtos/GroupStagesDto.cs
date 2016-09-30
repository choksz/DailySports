using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.ServiceLayer.Dtos
{
  public  class GroupStagesDto
    {
        public int Id { get; set; }
        public List<TeamDto> TeamList { get; set; }
        
    }
}
