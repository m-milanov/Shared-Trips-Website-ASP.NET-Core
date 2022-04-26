using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedTrips.Extensions;
using SharedTrips.Models.Cars;
using SharedTrips.Services.Cars;
using SharedTrips.Services.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Controllers
{
    public class CarsController : Controller
    {
        private readonly IDriversService drivers;
        private readonly ICarsService cars;

        public CarsController(IDriversService drivers, ICarsService cars)
        {
            this.drivers = drivers;
            this.cars = cars;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!drivers.UserIsDriver(this.User.GetId()))
            {
                return RedirectToAction(nameof(DriversController.Become), "Drivers");
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add(AddCarFormModel car)
        {
            if (!drivers.UserIsDriver(this.User.GetId()))
            {
                return RedirectToAction(nameof(DriversController.Become), "Driver");
            }

            if (!ModelState.IsValid)
            {
                return View(car);
            }

            var driverId = drivers.GetIdByUser(this.User.GetId());

            cars.Add(car.Brand, car.Model, car.Year, car.ImgUrl, driverId);

            return RedirectToAction("Index", "Home");
        }
    }
}
