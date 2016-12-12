using DailySports.ServiceLayer.Dtos;
using DailySports.ServiceLayer.IServices;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DailySports.Models;
using System.Net;
using DailySports.Helpers;

namespace DailySports.Controllers
{
    public class NewsController : Controller
    {
        private INewsService _newsService;
        private IPetService _petService;
        private IMatchService _matchService;
        private ITournementsService _tournamentService;
        public NewsController(INewsService newsService, IPetService petService, IMatchService matchService, ITournementsService tournamentService)
        {
            _newsService = newsService;
            _petService = petService;
            _matchService = matchService;
            _tournamentService = tournamentService;
        }
        // GET: News0
        public IActionResult Index()
        {
            NewsListDto newsDtoList = new NewsListDto();
            newsDtoList.AllNews = _newsService.GetAll();
            newsDtoList.Latest = _newsService.LatestNews();
            newsDtoList.NextMatches = _matchService.NextMatches(_tournamentService.GetLatestTornamentId());
            PetOfTheWeekDto pet = _petService.GetPetOfTheWeek();
            newsDtoList.PetOfTheWeek = pet;
            return View(newsDtoList);
        }
        [ServiceFilter(typeof(UrlEncode))]
        [Route("News/{title}")]
        public IActionResult GetNews(int id, string title)
        {
            NewsDto news = new NewsDto();
            news = _newsService.GetNews(id);
            //news.NextMatches = _matchService.NextMatches(_tournamentService.GetLatestTornamentId());
            PetOfTheWeekDto pet = _petService.GetPetOfTheWeek();
            news.PetOfTheWeek = pet;
            news.Title = WebUtility.HtmlDecode(news.Title); 
            return View(news);
        }
        public IActionResult Search(string Search)
        {
            NewsListDto newsList = new NewsListDto();
            newsList.AllNews = _newsService.SearchNews(Search);
            newsList.Latest = _newsService.LatestNews();
            newsList.NextMatches = _matchService.NextMatches(_tournamentService.GetLatestTornamentId());
            PetOfTheWeekDto pet = _petService.GetPetOfTheWeek();
            newsList.PetOfTheWeek = pet;
            if (newsList != null)
            {
                return View("Index", newsList);
            }
            else
            {
                return RedirectToAction("Index", "News");
            }
        }
    }
}