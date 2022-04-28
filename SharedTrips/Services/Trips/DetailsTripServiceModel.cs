using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Services.Trips
{
    public class DetailsTripServiceModel
    {
        public int Id { get; set; }

        public int Price { get; init; }

        public DateTime TimeOfDeparture { get; init; }

        public int MaxPassengers { get; set; }

        public string FromCityName { get; init; }

        public string ToCityName { get; init; }
    }
}
