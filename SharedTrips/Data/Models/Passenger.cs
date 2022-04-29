using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using static SharedTrips.Data.DataConstants.Passenger;

namespace SharedTrips.Data.Models
{
    public class Passenger : IdentityUser
    {
        [Required]
        [MaxLength(NameMaxLength)]
        public string FullName { get; set; }

        public int Age { get; set; }

        public List<Feedback> Feedbacks { get; set; }

        public List<Driver> DriversToRate { get; set; }

        public List<TripPassenger> TripPassengers { get; set; }

    }
}
