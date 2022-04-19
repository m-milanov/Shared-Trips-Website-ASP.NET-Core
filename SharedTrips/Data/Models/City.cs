using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Data.Models
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Trip> StartOfTrip { get; set; } = new List<Trip>();

        public IEnumerable<Trip> EndOfTrip { get; set; } = new List<Trip>();
    }
}
