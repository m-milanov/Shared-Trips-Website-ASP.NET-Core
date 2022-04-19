using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using static SharedTrips.Data.DataConstants;

namespace SharedTrips.Data.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(CarBrandMaxLength)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(CarModelMaxlength)]
        public string Model { get; set; }

        [Required]
        public string ImgUrl { get; set; }

        [Required]
        public int Year { get; set; }
    }
}
