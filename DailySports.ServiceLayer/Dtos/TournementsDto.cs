using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailySports.DataLayer.Model;

namespace DailySports.ServiceLayer.Dtos
{
   public class TournementsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Format { get; set; }
        public string Overview { get; set; }
        public string MainEvent { get; set; }
        public string Qualifiers { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public int GameId { get; set; }

        public GameDto Game { get; set; }
        public string TournamentImage { get; set; }
        public List<MatchDto> TournamentMatches { get; set; }
        public List<PrizePoolDto> TournamentPrizePool { get; set; }
        public List<GroupStagesDto> TournamentGroupStages { get; set; }
        public List<MatchDto> NextMatches { get; set; }
        public TournementsDto() {}
        public TournementsDto(Tournaments tournament)
        {
            Id = tournament.Id;
            Title = tournament.Title;
            Description = tournament.Description;
            Format = tournament.Format;
            MainEvent = tournament.MainEvent;
            GameId = tournament.GameId;
            Overview = tournament.Overview;
            Price = tournament.Price;
            Qualifiers = tournament.Qualifiers;
            StartDate = tournament.StartDate;
            EndDate = tournament.EndDate;
            URL = tournament.URL;
            TournamentImage = tournament.TournamentImage;
            Game = new GameDto(tournament.Game);
        }
    }
}
