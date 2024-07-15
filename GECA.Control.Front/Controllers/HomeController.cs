using GECA.Control.Front.Models;
using GECA_Control.Models;
using GECA_Control.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GECA.Control.Front.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpPost]
        public JsonResult Control(string direction,int step)
        {

            return Json(ApplicationService.SerializeCoordinatesArray(map.Matrix));
        }
        [HttpGet]
        public JsonResult GetMap() {
            Map map = new Map(30, 30);
            return Json(ApplicationService.SerializeCoordinatesArray(map.Matrix));
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
