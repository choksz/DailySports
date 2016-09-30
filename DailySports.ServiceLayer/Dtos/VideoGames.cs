using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.ServiceLayer.Dtos
{
   public class VideoGames
    {
        public List<VideoDto> videoList { get; set; }
       public  List<GameDto> GameList { get; set; }
    }
}
