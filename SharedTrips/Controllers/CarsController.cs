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
        public IActionResult Add(CarFormModel car)
        {
            if (!drivers.UserIsDriver(this.User.GetId()))
            {
                return RedirectToAction(nameof(DriversController.Become), "Drivers");
            }

            if (!ModelState.IsValid)
            {
                return View(car);
            }

            var driverId = drivers.GetIdByUser(this.User.GetId());

            cars.Add(car.Brand, car.Model, car.Year, car.ImgUrl, driverId);

            return RedirectToAction("UserCars", "Cars");
        }

        [Authorize]
        public IActionResult UserCars()
        {
            if (!drivers.UserIsDriver(this.User.GetId()))
            {
                return RedirectToAction(nameof(DriversController.Become), "Drivers");
            }

            var cars = this.cars.GetCarsForDriver(this.drivers.GetIdByUser(this.User.GetId()));

            return View(cars);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!drivers.UserIsDriver(userId))
            {
                return RedirectToAction(nameof(DriversController.Become), "Drivers");
            }

            var car = cars.GetCar(id);

            if (car.UserId != userId)
            {
                return Unauthorized();
            }

            return View(car);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, CarFormModel car)
        {
            var userId = this.User.GetId();
            var carData = cars.GetCar(id);

            if (carData.UserId != userId)
            {
                return Unauthorized();
            }

            if (!cars.UpdateCar(id, car.Brand, car.Model, car.Year, car.ImgUrl))
            {
                return BadRequest();
            }

            return RedirectToAction("UserCars", "Cars");
        }
    }
}
