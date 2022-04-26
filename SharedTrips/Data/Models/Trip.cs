using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using static SharedTrips.Data.DataConstants.Trip;

namespace SharedTrips.Data.Models
{
    public class Trip
    {
        public int Id { get; set; }

        public int MaxPassengers { get; set; }

        [Required]
        public DateTime TimeOfDeparture { get; set; }

        public int Price { get; set; }

        public int FromCityId { get; set; }

        public City FromCity { get; set; }

        public int ToCityId { get; set; }

        public City ToCity { get; set; }

        public int DriverId { get; set; }

        public Driver Driver { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
