using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using static SharedTrips.Data.DataConstants.Car;

namespace SharedTrips.Data.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(BrandMaxLength)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(ModelMaxLength)]
        public string Model { get; set; }

        [Required]
        [Url]
        public string ImgUrl { get; set; }

        [Required]
        public int Year { get; set; }

        public int DriverId { get; set; }

        public Driver Driver { get; set; }
    }
}
