using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Services.Trips
{
    public class PassengerServiceModel
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public bool Accepted { get; set; }
    }
}
