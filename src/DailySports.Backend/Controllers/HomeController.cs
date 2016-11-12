using Microsoft.AspNetCore.Mvc;

namespace DailySports.Backend.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}