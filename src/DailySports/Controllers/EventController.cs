using DailySports.ServiceLayer.Dtos;
using DailySports.ServiceLayer.IServices;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DailySports.Controllers
{
    public class EventController : Controller
    {
        private IEventService _eventService;
        private IPetService _petService;
        private IMatchService _matchService;
        private ITournementsService _tournamentService;
        public EventController(IEventService eventService,IPetService petService,IMatchService matchservice,ITournementsService tournamentService)
        {
            _eventService = eventService;
            _petService = petService;
            _tournamentService = tournamentService;
            _matchService = matchservice;

        }
        // GET: Event
        public IActionResult Index()
        {
            /*
           if (Session["LoggedInUser"] != null)
            {
                List<EventDto> allEvents = new List<EventDto>();
                allEvents = _eventService.GetAll();
                return View(allEvents);
            }
            else if (Response.Cookies["LoggedInUser"] != null)
            {
                List<EventDto> allEvents = new List<EventDto>();
                allEvents = _eventService.GetAll();
                return View(allEvents);
           }
          else 
            {
                return RedirectToAction("Login", "Home");
            }
            */
            List<EventDto> allEvents = new List<EventDto>();
            allEvents = _eventService.GetAll();
            return View(allEvents);
        }
        public IActionResult GetEvent(int id)
        {
            EventDto newEventDto = new EventDto();
            newEventDto = _eventService.GetEvent(id);
            newEventDto.NextMatches = new List<MatchDto>();//_matchService.NextMatches(_tournamentService.GetLatestTornamentId());
            PetOfTheWeekDto pet = _petService.GetPetOfTheWeek();
            newEventDto.PetOfTheWeekDto = pet;
            return View(newEventDto);
        }
        public IActionResult Search(string Search)
        {
            List<EventDto> EventDtoList = _eventService.SearchEvent(Search);
            if (EventDtoList != null)
            {
                return View("Index", EventDtoList);
            }
            else
            {
                return RedirectToAction("Index","Event");
            }
        }
    }
}