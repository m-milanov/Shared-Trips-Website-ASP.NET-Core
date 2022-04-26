using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Services.Trips
{
    public class TripDetailsServiceModel
    {
        public int Price { get; set; }

        public int MaxPassengers { get; set; }

        public DateTime TimeOfDeparture { get; set; }

        public string FromCityName { get; set; }

        public string ToCityName { get; set; }
    }
}
