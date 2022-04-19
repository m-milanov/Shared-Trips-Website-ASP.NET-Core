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

        public int FromCityId { get; init; }

        public int ToCityId { get; init; }
    }
}
