using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Services.Drivers
{
    public class DriverServiceModel
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfilePictureUrl { get; set; }

        public int TimesDriver { get; set; }

        public double AvrgRating { get; set; }

        public IEnumerable<FeedbackServiceModel> Feedbacks { get; set; }
    }
}
