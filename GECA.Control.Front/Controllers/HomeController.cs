using GECA.Control.Front.Models;
using GECA.Control.Front.Utile;
using GECA_Control.Models;
using GECA_Control.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GECA.Control.Front.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        [HttpPost]
        public JsonResult Control(string direction,int step)
        {
            var caterpillar = HttpContext.Session.Get<Caterpillar>("Caterpillar") ?? new Caterpillar();
            var map = HttpContext.Session.Get<Map>("Map") ?? new Map(30, 30);

            DoMove move = new DoMove(direction, step);
            CaterpillarControlService.MoveCaterpillar(caterpillar, map, move, _config["PathLogCommand"]);

            HttpContext.Session.Set("Caterpillar", caterpillar);
            HttpContext.Session.Set("Map", map);

            return Json(new { caterpillarJson = caterpillar, mapJson = ApplicationService.SerializeCoordinatesArray(Map.Matrix) });
        }
        [HttpGet]
        public JsonResult GetMap() {
            var caterpillar = HttpContext.Session.Get<Caterpillar>("Caterpillar") ?? new Caterpillar();
            var map = HttpContext.Session.Get<Map>("Map") ?? new Map(30, 30);
            HttpContext.Session.Set("Caterpillar", caterpillar);
            HttpContext.Session.Set("Map", map);
            return Json(new {caterpillarJson = caterpillar, mapJson = ApplicationService.SerializeCoordinatesArray(Map.Matrix) });
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
