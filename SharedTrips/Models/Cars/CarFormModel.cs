using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using static SharedTrips.Data.DataConstants.Car;


namespace SharedTrips.Models.Cars
{
    public class CarFormModel
    {
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
        [Range(YearMinVal, YearMaxVal)]
        public int Year { get; set; }
    }
}
