using System.Collections.Generic;

namespace DailySports.ServiceLayer.Dtos
{
    public class VideoList
    {
        public List<VideoDto> ThisWeekVideos { get; set; }
        public List<VideoDto> LastWeekVideos { get; set; }
        public List<GameDto> Games { get; set; }
        public List<CategoryDto> Categories { get; set; }
    }
}
