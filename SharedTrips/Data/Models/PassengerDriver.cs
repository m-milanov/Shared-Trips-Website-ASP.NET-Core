using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Data.Models
{
    public class PassengerDriver
    {
        public string PassengerId { get; set; }
        public Passenger Passenger { get; set; }

        public int DriverId { get; set; }
        public Driver Driver { get; set; }
    }
}
