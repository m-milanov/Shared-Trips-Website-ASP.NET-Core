using SharedTrips.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using static SharedTrips.Data.DataConstants.Driver;

namespace SharedTrips.Models.Drivers
{
    public class BecomeDriverFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        public string PhoneNumber { get; set; }

        [Url]
        public string ProfilePictureUrl { get; set; }

    }
}
