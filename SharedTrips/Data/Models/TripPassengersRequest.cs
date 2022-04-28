﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Data.Models
{
    public class TripPassengersRequest
    {
        public int TripId { get; set; }
        public Trip Trip { get; set; }

        public string PassengerId { get; set; }
        public Passenger Passenger { get; set; }
    }
}
