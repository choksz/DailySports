using DailySports.ServiceLayer.Dtos;
using DailySports.ServiceLayer.IServices;
using Microsoft.AspNetCore.Mvc;

namespace DailySports.Controllers
{
    public class TournamentController : Controller
    {
        private ITournementsService _tournamentService;
        private IMatchService _matchService;
        private IGameService _gameService;
        private INewsService _newsService;

        public TournamentController(ITournementsService tournamentService,
                                    IMatchService matchService,
                                    IGameService gameService,
                                    INewsService newsService)
        {
            _tournamentService = tournamentService;
            _matchService = matchService;
            _gameService = gameService;
            _newsService = newsService;
        }
        // GET: Tournament

        public IActionResult Index()
        {
            ModelState.Clear();

            TounamentListDto newTournamentList = new TounamentListDto();
            newTournamentList.AllTournaments = _tournamentService.GetAll();
            newTournamentList.LatestTournament = _tournamentService.LatestTournements();
            newTournamentList.AllGames = _gameService.GetAll();
            return View(newTournamentList);
        }

        public IActionResult GetTournament(int id)
        {
            TournementsDto TournamentDto = new TournementsDto();
            TournamentDto = _tournamentService.GetTournement(id);
            return View(TournamentDto);
        }

        public IActionResult NullModel() { return View(); }
    }
}