using DailySports.ServiceLayer.Dtos;
using DailySports.ServiceLayer.IServices;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DailySports.Models;

namespace DailySports.Controllers
{
    public class VideoController : Controller
    {
        private IVideoService _videoService;
        private IGameService _gameService;
        private IPetService _petService;
        private IMatchService _matchService;
        private ICategoryService _categoryService;
        public VideoController(IVideoService videoService,ICategoryService categoryService,IMatchService matchService,IGameService gameService,IPetService petService)
        {
            _videoService = videoService;
            _gameService = gameService;
            _petService = petService;
            _matchService = matchService;
            _categoryService = categoryService;
        }
        // GET: Video
        public IActionResult Index()
        {
            if (true/*Session["LoggedInUser"] != null*/)
            {
                VideoList videogame = new VideoList();
                videogame.ThisWeekVideos = _videoService.GetAll();
                videogame.Games = _gameService.GetAll();
                videogame.LastWeekVideos = _videoService.GetLastWeekVideos();
                videogame.Categories = _categoryService.GetAll();
                return View(videogame);
            }
            else if (Request.Cookies["LoggedInUser"] != null)
            {
                VideoList videogame = new VideoList();
                videogame.ThisWeekVideos = _videoService.GetAll();
                videogame.Games = _gameService.GetAll();
                videogame.LastWeekVideos = _videoService.GetLastWeekVideos();
                videogame.Categories = _categoryService.GetAll();
                return View(videogame);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public IActionResult CategoryVideos(int id)
        {
            VideoList videogame = new VideoList();
            videogame.ThisWeekVideos = _videoService.GetCategoryVideos(id);
            videogame.Games = _gameService.GetAll();
            videogame.LastWeekVideos = _videoService.GetLastWeekVideos();
            videogame.Categories = _categoryService.GetAll();
            return View("Index",videogame);

        }
        public IActionResult GetVideoGames(int id)
        {
            VideoGames newVideoGame = new VideoGames();
            newVideoGame.GameList = _gameService.GetAll();
            newVideoGame.videoList = _videoService.GetGamesVideos(id);
            if (newVideoGame != null)
            {
                return View("Index", newVideoGame);
            }
            else
            {
                return RedirectToAction("Index", "Video");
            }
        }
        public IActionResult GetVideo(int id)
        {
            VideoDto newVideo = new VideoDto();
            newVideo = _videoService.GetVideo(id);
            List<PetOfTheWeekDto> pets = new List<PetOfTheWeekDto>();
            pets = _petService.GetPetOfTheWeek();
            if(pets.Count != 0)
            {
                newVideo.petOfTheDay = pets;
            }
            else
            {
                DefaultPet defaultpet = new DefaultPet();
                pets.Add(new PetOfTheWeekDto
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
                newVideo.petOfTheDay = pets;
            }
            newVideo.NextMatches = _matchService.NextMatches(newVideo.TournamentID);
            return View(newVideo);
        }
        public ActionResult Search(string Search)
        {
            VideoGames VideoSearch = new VideoGames();

            VideoSearch.videoList = _videoService.Search(Search);
            return View("Index", VideoSearch);
        }
    }
}