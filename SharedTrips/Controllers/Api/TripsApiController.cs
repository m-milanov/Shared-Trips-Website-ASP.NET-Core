using Microsoft.AspNetCore.Mvc;
using SharedTrips.Services.Trips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Controllers.Api
{
    [ApiController]
    [Route("api/trips")]
    public class TripsApiController : ControllerBase
    {
        private readonly ITripsService trips;

        public TripsApiController(ITripsService trips)
        {
            this.trips = trips;
        }

        [HttpGet]
        public IEnumerable<TripServiceListingModel> All()
            => this.trips.GetTrips(0, 0, new DateTime()).Trips;

        [HttpGet]
        [Route("{id}")]
        public DetailsTripServiceModel All(int id)
            => this.trips.GetTripDetails(id);

    }
}
