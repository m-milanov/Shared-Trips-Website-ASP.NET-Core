using SharedTrips.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Models.Trips
{
    public class TripListingViewModel
    {
        public int Id { get; init; }

        public int Price { get; init; }

        public DateTime TimeOfDeparture { get; init; }

        public int MaxPassengers { get; set; }

        public string FromCity { get; init; }

        public string ToCity { get; init; }

        public DriverListingViewModel DriverListingViewModel { get; set; }

    }
}
