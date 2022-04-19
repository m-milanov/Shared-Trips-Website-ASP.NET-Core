using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public int FromCityId { get; set; }

        public City FromCity { get; set; }

        public int ToCityId { get; set; }

        public City ToCity { get; set; }

    }
}
