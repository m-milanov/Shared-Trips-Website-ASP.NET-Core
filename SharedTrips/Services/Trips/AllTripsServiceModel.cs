using SharedTrips.Models.Cities;
using SharedTrips.Models.Trips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Services.Trips
{
    public class AllTripsServiceModel
    {
        public int TotalTrips { get; set; } = 1;

        public IEnumerable<CityServiceModel> Cities { get; set; }

        public List<TripServiceListingModel> Trips { get; set; }
    }
}
