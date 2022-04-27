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
                return RedirectToAction(nameof(DriversController.Become), "Drivers");
            }

            if (!ModelState.IsValid)
            {
                return View(car);
            }

            var driverId = drivers.GetIdByUser(this.User.GetId());

            cars.Add(car.Brand, car.Model, car.Year, car.ImgUrl, driverId);

            return RedirectToAction("Cars", "UserCars");
        }

        [Authorize]
        public IActionResult UserCars()
        {
            if (!drivers.UserIsDriver(this.User.GetId()))
            {
                return RedirectToAction(nameof(DriversController.Become), "Drivers");
            }

            var cars = this.cars
                .GetCarsForDriver(this.drivers.GetIdByUser(this.User.GetId()))
                .Select(c => new CarViewModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    ImgUrl = c.ImgUrl
                });

            return View(cars);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            if(!this.cars.GetCarsForDriver(this.drivers.GetIdByUser(this.User.GetId())).Any(c => c.Id == id))
            {
                return BadRequest();
            }

            var car = cars.GetCar(id);

            return View(
            new EditCarFormModel
            {
                Id = id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                ImgUrl = car.ImgUrl
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(EditCarFormModel car)
        {
            if (!this.cars.GetCarsForDriver(this.drivers.GetIdByUser(this.User.GetId())).Any(c => c.Id == car.Id))
            {
                return BadRequest();
            }

            if (!cars.SaveCar(car.Id, car.Brand, car.Model, car.Year, car.ImgUrl))
            {
                return BadRequest();
            }

            return RedirectToAction("UserCars", "Cars");
        }
    }
}
