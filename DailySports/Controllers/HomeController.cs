using DailySports.App_Start;
using DailySports.Models;
using DailySports.ServiceLayer.Dtos;
using DailySports.ServiceLayer.IServices;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using DailySports.ServiceLayer.Utilities;

namespace DailySports.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private ILatestService _LatestService;
        private IUserService _userService;
        private IMatchService _matchService;
        private ITournementsService _tournamentService;
        private IPetService _petService;
        private IGameService _gameservice;
        public HomeController(ILatestService LatestService, IGameService gameService, IPetService petService, IMatchService matchService, ITournementsService tournamentService, IUserService userService)
        {
            _LatestService = LatestService;
            _userService = userService;
            _matchService = matchService;
            _tournamentService = tournamentService;
            _petService = petService;
            _gameservice = gameService;
        }

        public ActionResult Index()
        {
            LatestDto Latest = new LatestDto();
            Latest.LatestEvents = _LatestService.GetLatestEvents();
            Latest.LatestNews = _LatestService.GetLatestNews();
            Latest.LatestTournament = _LatestService.GetLatestTournaments();
            Latest.LatestVideos = _LatestService.GetLatestVideos();
            Latest.NextMatches = _matchService.NextMatches(_tournamentService.GetLatestTornamentId());
            Latest.LiveGames = _gameservice.GetAll();
            Latest.OngoingTournaments = _LatestService.GetOngoingTournaments();
            List<PetOfTheWeekDto> pet = new List<PetOfTheWeekDto>();
            pet = _petService.GetPetOfTheWeek();
            if (pet.Count != 0)
            {
                Latest.PetOfTheWeek = _petService.GetPetOfTheWeek();
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
                Latest.PetOfTheWeek = pet;
            }
            return View(Latest);
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult AddUser(UserDto user, HttpPostedFileBase file)
        {

            UserDto newUser = new UserDto();
            newUser = _userService.GetUserByEmail(user.Email);
            if (newUser != null)
            {
                ModelState.AddModelError("", "Email already Exists please choose another email");
                return View("Register");
            }
            else
            {
                user.Password = PasswordHelper.ComputeHash(user.Password, "SHA512", null);
                bool result = _userService.AddUser(user, file);
                if (result == true)
                {
                    Session["LoggedInUser"] = user;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Cant Resgister now Please try again later");
                    return View("Register");
                }
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Signin(UserDto user)
        {
            UserDto LoggedInUser = new UserDto();
            var tempUser = _userService.GetUserByEmail(user.Email);
            var flag = PasswordHelper.VerifyHash(user.Password, "SHA512", tempUser.Password);

            if (tempUser != null && flag == true)
            {
                LoggedInUser = tempUser;
            }
            if (LoggedInUser != null && user.rememberme == true)
            {
                Session["LoggedInUser"] = LoggedInUser;

                HttpCookie cookie = new HttpCookie("LoggedInUser");
                cookie.Values.Add("Email", user.Email);
                cookie.Values.Add("UserName", user.Name);
                cookie.Expires = DateTime.Now.AddDays(20);
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "Home");
            }
            else if (LoggedInUser != null)
            {
                Session["LoggedInUser"] = LoggedInUser;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View("Login");
            }
        }
        public ActionResult Logout()
        {
            Session["LoggedInUser"] = null;
            if (Request.Cookies["LoggedInUser"] != null)
            {
                var c = new HttpCookie("LoggedInUser");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }

        public ActionResult ChangePassword(string Email, string SecurityCode)
        {
            UserDto newUser = new UserDto();
            newUser = _userService.GetUser(Email, SecurityCode);
            if (newUser != null)
            {
                return View("ResetPassword");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Information !! ");
                return View("ForgetPassword");
            }
        }
        public ActionResult ResetPassword() { return View(); }
        public ActionResult SettingPassword(string Email, string Password)
        {
            bool result = _userService.ChangePassword(Email, Password);
            if (result == true)
            {
                ModelState.AddModelError("", "Please Login with the new Password");
                return View("Login");

            }
            else
            {
                ModelState.AddModelError("", "Something Went wrong Please try again later");
                return View("ResetPassword");
            }
        }

    }
}