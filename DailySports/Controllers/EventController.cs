using DailySports.Models;
using DailySports.ServiceLayer.Dtos;
using DailySports.ServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Index()
        {
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
        }
        public ActionResult GetEvent(int id)
        {
            EventDto newEventDto = new EventDto();
            newEventDto = _eventService.GetEvent(id);
            newEventDto.NextMatches = _matchService.NextMatches(_tournamentService.GetLatestTornamentId());
            
            List< PetOfTheWeekDto> pet = new List<PetOfTheWeekDto>();
            pet = _petService.GetPetOfTheWeek();
            if(pet.Count !=0)
            {
                newEventDto.petOfTheDay = pet;

            }
            else
            {
                DefaultPet defaultpet = new DefaultPet();
                pet.Add(new PetOfTheWeekDto
                {
                    Id = defaultpet.Id,
                    Title = defaultpet.Name,
                    Age = defaultpet.Age,
                    Description = defaultpet.Description,
                    FunFact = defaultpet.FunFact,
                    Gender = defaultpet.Gender,
                    Owner = defaultpet.Owner,
                    PetImage = defaultpet.Image
                });
                newEventDto.petOfTheDay = pet;
            }


            return View(newEventDto);
        }
        public ActionResult Search(string Search)
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