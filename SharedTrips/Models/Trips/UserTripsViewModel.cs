using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Models.Trips
{
    public class UserTripsViewModel
    {
        public bool UserIsDriver { get; set; }

        public List<TripListingViewModel> TripsAsDriver { get; set; }

        public List<TripListingViewModel> TripsAsPassenger { get; set; }
    }
}
