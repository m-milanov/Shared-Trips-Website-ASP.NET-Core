using SharedTrips.Models.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Models.Trips
{
    public class AllTripsViewModel
    {
        public int FromCityId { get; set; }

        public int ToCityId { get; set; } 

        public DateTime TimeOfDeparture { get; set; }

        public IEnumerable<CityViewModel> Cities { get; set; }

        public List<TripListingViewModel> Trips { get; set; }
    }
}
