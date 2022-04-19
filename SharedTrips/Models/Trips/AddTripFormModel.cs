﻿using SharedTrips.Data.Models;
using SharedTrips.Models.Cities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using static SharedTrips.Data.DataConstants;


namespace SharedTrips.Models.Trips
{
    public class AddTripFormModel
    {
        [MaxLength(MaxTripPassengers)]
        [MinLength(MinTripPassengers)]
        public int MaxPassengers { get; init; }

        [Required]
        public DateTime TimeOfDeparture { get; init; }

        [MaxLength(MaxTripPrice)]
        [MinLength(MinTripPrice)]
        public int Price { get; init; }

        public int FromCityId { get; set; }

        public int ToCityId { get; set; }

        public IEnumerable<CityViewModel> Cities { get; set; }
    }
}