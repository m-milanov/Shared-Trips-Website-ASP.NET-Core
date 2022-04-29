using Microsoft.AspNetCore.Mvc;
using SharedTrips.Models;
using SharedTrips.Services.Drivers;
using System.Diagnostics;

namespace SharedTrips.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDriversService drivers;

        public HomeController(IDriversService drivers)
        {
            this.drivers = drivers;
        }


        public IActionResult Index()
        {
            return View(this.drivers.GetTopDrivers());
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
