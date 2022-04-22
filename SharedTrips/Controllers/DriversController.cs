using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedTrips.Data;
using SharedTrips.Data.Models;
using SharedTrips.Extensions;
using SharedTrips.Models.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Controllers
{
    public class DriversController : Controller
    {
        private readonly SharedTripsDbContext data;

        public DriversController(SharedTripsDbContext data)
            => this.data = data;

        public IActionResult Become()
        {

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeDriverFormModel driver)
        {
            if (UserIsDriver())
            {
                return BadRequest();
            }

            if(!ModelState.IsValid)
            {
                return View(driver);
            }

            var driverData = new Driver
            {
                Name = driver.Name,
                PhoneNumber = driver.PhoneNumber,
                ProfilePictureUrl = driver.ProfilePictureUrl,
                UserId = this.User.GetId()
            };

            this.data.Drivers.Add(driverData);
            this.data.SaveChanges();

            return RedirectToAction("Index","Home");
        }

        private bool UserIsDriver()
           => this.data.Drivers
               .Any(d => d.UserId == this.User.GetId());
    }
}
