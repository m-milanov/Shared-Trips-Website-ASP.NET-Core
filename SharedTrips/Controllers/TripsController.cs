using Microsoft.AspNetCore.Mvc;
using SharedTrips.Data;
using SharedTrips.Models.Cities;
using SharedTrips.Models.Trips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Controllers
{
    public class TripsController : Controller
    {
        private readonly SharedTripsDbContext data;

        public TripsController(SharedTripsDbContext data)
        {
            this.data = data;
        }

        public IActionResult Add()
        {
            return View(new AddTripFormModel
            {
                Cities = this.GetCities()
            });

        }
        
        [HttpPost]
        public IActionResult Add(AddTripFormModel trip)
        {
            return View();
        }

        private IEnumerable<CityViewModel> GetCities()
            => this.data.Cities
            .Select(c => new CityViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();
    }

}
