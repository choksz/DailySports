using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GameImage { get; set; }
        public string LiveStreamURL { get; set; }

        public GameDto() { }
        
        public GameDto(Game game)
        {
            Id = game.Id;
            Name = game.Name;
            GameImage = game.GameImage;
            LiveStreamURL = game.LiveStreamUrl;
        }
    }
}
