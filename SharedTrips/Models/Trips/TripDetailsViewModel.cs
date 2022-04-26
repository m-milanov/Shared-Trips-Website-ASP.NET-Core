using SharedTrips.Services.Cars;
using SharedTrips.Services.Drivers;
using SharedTrips.Services.Trips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Models.Trips
{
    public class TripDetailsViewModel
    {
        public TripDetailsServiceModel Trip { get; set; }

        public CarServiceModel Car { get; set; }

        public DriverServiceModel Driver { get; set; }
    }
}
