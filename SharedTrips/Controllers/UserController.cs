using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedTrips.Extensions;
using SharedTrips.Services.Trips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Controllers
{
    public class UsersController : Controller
    {
        private readonly ITripsService trips;

        public UsersController(ITripsService trips)
        {
            this.trips = trips;
        }

        [Authorize]
        public IActionResult UserRequest(int id)
        {
            if (this.trips.UserIsDriver(id, this.User.GetId()))
            {
                return Unauthorized();
            }

            var userId = this.User.GetId();

            this.trips.UserRequest(id, userId);

            return RedirectToAction("Details", "Trips", new { id });
        }

        [Authorize]
        public IActionResult AcceptUser(int id, string userId)
        {
            if (!this.trips.UserIsDriver(id, this.User.GetId()))
            {
                return Unauthorized();
            }

            this.trips.AcceptRequest(id, userId);

            return RedirectToAction("Details", "Trips", new { id });
        }

        [Authorize]
        public IActionResult RemoveUser(int id, string userId)
        {
            if (!this.trips.UserIsDriver(id, this.User.GetId()))
            {
                return Unauthorized();
            }

            this.trips.RemoveUser(id, userId);

            return RedirectToAction("Details", "Trips", new { id });
        }
    }
}
