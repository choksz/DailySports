using System;
using System.Collections.Generic;
using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
    public class TournementsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public string URL { get; set; }
        public string Venue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TournamentImage { get; set; }

        public int PrizePoolId { get; set; }
        public PrizePoolDto PrizePool { get; set; }
        
        public int GameId { get; set; }
        public GameDto Game { get; set; }
        
        public List<NewsDto> News { get; set; }
        
        public List<StageDto> Stages { get; set; }
        public List<StreamDto> Streams { get; set; }

        public TournementsDto() { }
        public TournementsDto(Tournaments tournament)
        {
            Id = tournament.Id;
            Title = tournament.Title;
            Overview = tournament.Overview;
            URL = tournament.URL;
            Venue = tournament.Venue;
            StartDate = tournament.StartDate;
            EndDate = tournament.EndDate;
            TournamentImage = tournament.TournamentImage;

            PrizePoolId = tournament.PrizePoolId;
            PrizePool = (tournament.PrizePool != null) ? new PrizePoolDto(tournament.PrizePool) : null;

            GameId = tournament.GameId;
            Game = (tournament.Game != null) ? new GameDto(tournament.Game) : null;

            News = new List<NewsDto>();
            if (tournament.News != null) {
                foreach (var news in tournament.News)
                {
                    News.Add(new NewsDto(news));
                }
            }

            Stages = new List<StageDto>();
            if (tournament.Stages != null)
            {
                foreach (var stage in tournament.Stages)
                {
                    Stages.Add(new StageDto(stage));
                }
            }

            Streams = new List<StreamDto>();
            if (tournament.Streams != null)
            {
                foreach (var stream in tournament.Streams)
                {
                    Streams.Add(new StreamDto(stream));
                }
            }
        }
    }
}
