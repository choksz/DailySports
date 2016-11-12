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
        public TournamentController(ITournementsService tournamentService, IMatchService matchService, IGameService gameService)
        {
            _tournamentService = tournamentService;
            _matchService = matchService;
            _gameService = gameService;
        }
        // GET: Tournament

        public IActionResult Index()
        {
            ModelState.Clear();
            if (true/*Session["LoggedInUser"] != null*/)
            {
                TounamentListDto newTournamentList = new TounamentListDto();
                newTournamentList.AllTournaments = _tournamentService.GetAll();
                newTournamentList.LatestTournament = _tournamentService.LatestTournements();
                newTournamentList.AllGames = _gameService.GetAll();
                if (newTournamentList.AllTournaments.Count != 0 && newTournamentList.LatestTournament != null)
                {
                    return View(newTournamentList);
                }
                else
                {
                    return RedirectToAction("NullModel", "News");
                }
            }
            else if (true/*Response.Cookies["LoggedInUser"] != null*/)
            {
                TounamentListDto newTournamentList = new TounamentListDto();
                newTournamentList.AllTournaments = _tournamentService.GetAll();
                newTournamentList.LatestTournament = _tournamentService.LatestTournements();
                newTournamentList.AllGames = _gameService.GetAll();
                if (newTournamentList.AllTournaments.Count != 0 && newTournamentList.LatestTournament != null)
                {
                    return View(newTournamentList);
                }
                else
                {
                    //ModelState.AddModelError("","No Data to display");
                    return View(newTournamentList);
                }
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public IActionResult GetTournament(int id)
        {
            TournementsDto TournamentDto = new TournementsDto();
            TournamentDto = _tournamentService.GetTournement(id);
            TournamentDto.TournamentMatches = _matchService.TournamentMatches(id);
            TournamentDto.NextMatches = _matchService.NextMatches(id);
            TournamentDto.TournamentPrizePool = _tournamentService.TournametPrizePool(id);
            TournamentDto.TournamentGroupStages = _tournamentService.TournamentGroupStages(id);
            return View(TournamentDto);
        }

        public IActionResult NullModel() { return View(); }
    }
}