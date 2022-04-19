using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using static SharedTrips.Data.DataConstants;

namespace SharedTrips.Data.Models
{
    public class Trip
    {
        public int Id { get; set; }

        [MaxLength(MaxTripPassengers)]
        public int MaxPassengers { get; set; }

        [Required]
        public DateTime TimeOfDeparture { get; set; }

        [MaxLength(MaxTripPrice)]
        public int Price { get; set; }

        [Required]
        public IEnumerable<City> Cities { get; set; }
    }
}
