using SharedTrips.Models.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Models.Trips
{
    public class AllTripsViewModel
    {
        public int TripsPerPage { get; set; } = 2;

        public int FromCityId { get; set; }

        public int ToCityId { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalTrips { get; set; } = 1;

        public DateTime TimeOfDeparture { get; set; }

        public IEnumerable<CityViewModel> Cities { get; set; }

        public List<TripListingViewModel> Trips { get; set; }
    }
}
