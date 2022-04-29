using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Services.Users
{
    public class RateDriverServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ProfilePictureUrl { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }
    }
}
