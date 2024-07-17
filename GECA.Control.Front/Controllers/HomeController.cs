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
        public JsonResult GrowCaterpillar(int statusGrow)
        {
            var caterpillar = HttpContext.Session.Get<Caterpillar>("Caterpillar") ?? new Caterpillar();
            var map = HttpContext.Session.Get<Map>("Map") ?? new Map(30, 30);
            Caterpillar.controlObstacle = "";
            caterpillar.GrowCaterpillar(statusGrow);
            if (caterpillar.Intermediate.Count > 0)
            {
                caterpillar.Tail.Value = '0';
                caterpillar.UpdateMatrix();
                caterpillar.Intermediate[caterpillar.Intermediate.Count - 1].Value = 'T';
            }
            HttpContext.Session.Set("Caterpillar", caterpillar);
            HttpContext.Session.Set("Map", map);
            return Json(new { caterpillarJson = caterpillar, mapJson = ApplicationService.SerializeCoordinatesArray(Map.Matrix), message = Caterpillar.controlObstacle });
        }
        [HttpPost]
        public JsonResult Control(string direction,int step)
        {
            var caterpillar = HttpContext.Session.Get<Caterpillar>("Caterpillar") ?? new Caterpillar();
            var map = HttpContext.Session.Get<Map>("Map") ?? new Map(30, 30);
            Caterpillar.controlObstacle = "";
            DoMove move = new DoMove(direction, step);
            CaterpillarControlService.MoveCaterpillar(caterpillar, map, move, _config["PathLogCommand"]);
            if (caterpillar.Intermediate.Count > 0)
            {
                caterpillar.Tail.Value = '0';
                caterpillar.UpdateMatrix();
                caterpillar.Intermediate[caterpillar.Intermediate.Count - 1].Value = 'T';
            }
            HttpContext.Session.Set("Caterpillar", caterpillar);
            HttpContext.Session.Set("Map", map);

            return Json(new { caterpillarJson = caterpillar, mapJson = ApplicationService.SerializeCoordinatesArray(Map.Matrix) ,message = Caterpillar.controlObstacle});
        }
        [HttpGet]
        public JsonResult GetMap() {
            HttpContext.Session.Clear();
            var caterpillar =  new Caterpillar();
            var map = new Map(30, 30);
            Caterpillar.controlObstacle = "";
            HttpContext.Session.Set("Caterpillar", caterpillar);
            HttpContext.Session.Set("Map", map);
            return Json(new {caterpillarJson = caterpillar, mapJson = ApplicationService.SerializeCoordinatesArray(Map.Matrix) });
        }

        public IActionResult Index()
        {
            HttpContext.Session.Clear();
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
