using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public class StreamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public bool Main { get; set; }

        public string LanguageCode { get; set; }
        public LanguageDto Language { get; set; }
        
        public int TournamentId { get; set; }

        public StreamDto() { }
        public StreamDto(Stream stream)
        {
            Id = stream.Id;
            Name = stream.Name;
            URL = stream.URL;
            Main = stream.Main;
            LanguageCode = stream.LanguageCode;
            Language = (stream.Language != null) ? new LanguageDto(stream.Language) : null;
            TournamentId = stream.TournamentId;
        }
    }
}
