using SharedTrips.Services.Cars;
using SharedTrips.Services.Trips;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using static SharedTrips.Data.DataConstants.Trip;

namespace SharedTrips.Models.Trips
{
    public class EditTripViewModel
    {
        [Range(MinTripPassengers, MaxTripPassengers)]
        public int MaxPassengers { get; init; }

        [Required]
        public DateTime TimeOfDeparture { get; init; }

        [Range(MinPrice, MaxPrice)]
        public int Price { get; init; }

        public int FromCityId { get; set; }

        public int ToCityId { get; set; }

        public int CarId { get; set; }

        public IEnumerable<CarServiceModel> Cars { get; set; }

        public IEnumerable<CityServiceModel> Cities { get; set; } = new List<CityServiceModel>();
    }
}
