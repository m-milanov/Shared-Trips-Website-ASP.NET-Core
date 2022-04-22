using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Data.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public int DriverId { get; set; }

        public Driver Driver { get; set; }
    }
}
