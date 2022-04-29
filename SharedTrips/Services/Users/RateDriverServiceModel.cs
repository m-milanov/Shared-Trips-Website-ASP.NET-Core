using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Services.Users
{
    public class RateDriverServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProfilePictureUrl { get; set; }

        [Range(0, 5)]
        [Required]
        public int Rating { get; set; }

        public string Comment { get; set; }
    }
}
