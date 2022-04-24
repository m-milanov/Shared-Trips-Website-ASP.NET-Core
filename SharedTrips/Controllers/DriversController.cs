using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedTrips.Data;
using SharedTrips.Data.Models;
using SharedTrips.Extensions;
using SharedTrips.Models.Drivers;
using SharedTrips.Services.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Controllers
{
    public class DriversController : Controller
    {
        private readonly IDriversService drivers;

        public DriversController(IDriversService drivers)
            => this.drivers = drivers;

        public IActionResult Become()
            => View();



        [HttpPost]
        [Authorize]
        public IActionResult Become(BecomeDriverFormModel driver)
        {
            if (drivers.UserIsDriver(this.User.GetId()))
            {
                return BadRequest();
            }

            if(!ModelState.IsValid)
            {
                return View(driver);
            }

            drivers.AddDriver(
                driver.Name,
                driver.PhoneNumber,
                driver.ProfilePictureUrl,
                this.User.GetId());

            return RedirectToAction("Index","Home");
        }
    }
}
