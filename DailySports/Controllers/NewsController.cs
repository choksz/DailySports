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
    public class NewsController : Controller
    {
        private INewsService _newsService;
        private IPetService _petService;
        private IMatchService _matchService;
        private ITournementsService _tournamentService;
        public NewsController(INewsService newsService,IPetService petService,IMatchService matchService,ITournementsService tournamentService)
        {
            _newsService = newsService;
            _petService = petService;
            _matchService = matchService;
            _tournamentService = tournamentService;
        }
        // GET: News0
        public ActionResult Index()
        {
            if (Session["LoggedInUser"] != null)
            {
                NewsListDto newsDtoList = new NewsListDto();
                newsDtoList.AllNews = _newsService.GetAll();
                newsDtoList.Latest = _newsService.LatestNews();
                newsDtoList.NextMatches = _matchService.NextMatches(_tournamentService.GetLatestTornamentId());
                List<PetOfTheWeekDto> pet = new List<PetOfTheWeekDto>();
                pet = _petService.GetPetOfTheWeek();
                if (pet.Count != 0)
                {
                    newsDtoList.PetOfTheDay = _petService.GetPetOfTheWeek();
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
                    newsDtoList.PetOfTheDay = pet;
                }
                    return View(newsDtoList);
            }
            else if (Response.Cookies["LoggedInUser"] != null)
            {
                NewsListDto newsDtoList = new NewsListDto();
                newsDtoList.AllNews = _newsService.GetAll();
                newsDtoList.Latest = _newsService.LatestNews();
                newsDtoList.NextMatches = _matchService.NextMatches(_tournamentService.GetLatestTornamentId());
                List<PetOfTheWeekDto> pet = new List<PetOfTheWeekDto>();
                pet = _petService.GetPetOfTheWeek();
                if (pet.Count != 0)
                {
                    newsDtoList.PetOfTheDay = _petService.GetPetOfTheWeek();
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
                    newsDtoList.PetOfTheDay = pet;
                }
                return View(newsDtoList);

            }
            else
            {
                return RedirectToAction("Login","Home");
            }
        }
       
        public ActionResult GetNews(int id)
        {
            NewsDto news = new NewsDto();
            news = _newsService.GetNews(id);
            news.NextMatches = _matchService.NextMatches(_tournamentService.GetLatestTornamentId());
           List<PetOfTheWeekDto> pet = new List<PetOfTheWeekDto>();
            pet = _petService.GetPetOfTheWeek();
            if (pet.Count != 0)
            {
                news.PetOfTheDate = _petService.GetPetOfTheWeek();
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
                news.PetOfTheDate = pet;
            }
            return View(news);
        }
        public ActionResult Search(string Search)
        {
            NewsListDto newsList = new NewsListDto();
            newsList.AllNews = _newsService.SearchNews(Search);
            newsList.Latest = _newsService.LatestNews();
            newsList.NextMatches = _matchService.NextMatches(_tournamentService.GetLatestTornamentId());
            List<PetOfTheWeekDto> pet = new List<PetOfTheWeekDto>();
            pet = _petService.GetPetOfTheWeek();
            if (pet.Count != 0)
            {
                newsList.PetOfTheDay = _petService.GetPetOfTheWeek();
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
                newsList.PetOfTheDay = pet;
            }

            if (newsList != null)
            {
                return View("Index",newsList);
            }
            else
            {
                return RedirectToAction("Index","News");
            }
        }
    }
}