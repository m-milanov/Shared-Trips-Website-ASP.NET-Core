using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Services.Trips
{
    public class TripServiceListingModel
    {
        public int Id { get; set; }

        public int Price { get; init; }

        public DateTime TimeOfDeparture { get; init; }

        public int MaxPassengers { get; set; }

        public string FromCity { get; init; }

        public string ToCity { get; init; }

        public string DriverName { get; set; }

        public string DriverPictureUrl { get; set; }

        public int DriverRating { get; set; }

        public int CarId { get; set; }
    }
}
