using DailySports.ServiceLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailySports.ServiceLayer.IServices
{
   public interface IVideoService :IDisposable
    {
        List<VideoDto> GetAll();
        VideoDto GetVideo(int id);
        List<VideoDto> GetGamesVideos(int GameId);
        List<VideoDto> GetLastWeekVideos();
        List<VideoDto> Search(string search);
        List<VideoDto> GetCategoryVideos(int CategoryId);
    }
}
