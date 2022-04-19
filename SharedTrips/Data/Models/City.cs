using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Data.Models
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Trip> Trips { get; set; } = new List<Trip>();
    }
}
