using DailySports.ServiceLayer.Dtos;
using DailySports.ServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DailySports.Controllers
{
    public class TournamentController : Controller
    {
        private ITournementsService _tournamentService;
        private IMatchService _matchService;
        public TournamentController(ITournementsService tournamentService, IMatchService matchService)
        {
            _tournamentService = tournamentService;
            _matchService = matchService;
        }
        // GET: Tournament

        public ActionResult Index()
        {
            ModelState.Clear();
            if (Session["LoggedInUser"] != null)
            {
                TounamentListDto newTournamentList = new TounamentListDto();
                newTournamentList.AllTournaments = _tournamentService.GetAll();
                newTournamentList.LatestTournament = _tournamentService.LatestTournements();
                if (newTournamentList.AllTournaments.Count != 0 && newTournamentList.LatestTournament != null)

                {
                    return View(newTournamentList);
                }
                else
                {
                    return RedirectToAction("NullModel", "News");
                }
            }
            else if (Response.Cookies["LoggedInUser"] != null)
            {
                TounamentListDto newTournamentList = new TounamentListDto();
                newTournamentList.AllTournaments = _tournamentService.GetAll();
                newTournamentList.LatestTournament = _tournamentService.LatestTournements();
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

        public ActionResult GetTournament(int id)
        {
            TournementsDto TournamentDto = new TournementsDto();
            TournamentDto = _tournamentService.GetTournement(id);
            TournamentDto.TournamentMatches = _matchService.TournamentMatches(id);
            TournamentDto.NextMatches = _matchService.NextMatches(id);
            TournamentDto.TournamentPrizePool = _tournamentService.TournametPrizePool(id);
            TournamentDto.TournamentGroupStages = _tournamentService.TournamentGroupStages(id);
            return View(TournamentDto);
        }

        public ActionResult NullModel() { return View(); }
    }
}