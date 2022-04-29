using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using static SharedTrips.Data.DataConstants.Driver;

namespace SharedTrips.Data.Models
{
    public class Driver
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } 

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        [Url]
        public string ProfilePictureUrl { get; set; }

        public int TimesDriver { get; set; }

        public IEnumerable<Feedback> Feedbacks { get; set; }

        public IEnumerable<Car> Cars { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Trip> Trips { get; set; }

        public List<PassengerDriver> PassengerDrivers { get; set; } = new List<PassengerDriver>();
    }
}
