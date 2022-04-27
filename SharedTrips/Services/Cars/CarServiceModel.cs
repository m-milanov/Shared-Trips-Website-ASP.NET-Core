using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Services.Cars
{
    public class CarServiceModel
    {
        public int Id{ get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string ImgUrl { get; set; }

        public string UserId { get; set; }
    }
}
