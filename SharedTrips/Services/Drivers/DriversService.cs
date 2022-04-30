using SharedTrips.Data;
using SharedTrips.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Services.Drivers
{
    public class DriversService : IDriversService
    {
        private readonly SharedTripsDbContext data;

        public DriversService(SharedTripsDbContext data)
        {
            this.data = data;
        }

        public int AddDriver(string name, string phoneNumber, string profilePictureUrl, string userId)
        {
            var driverData = new Driver
            {
                Name = name,
                PhoneNumber = phoneNumber,
                ProfilePictureUrl = profilePictureUrl,
                UserId = userId
            };

            this.data.Drivers.Add(driverData);
            this.data.SaveChanges();

            return driverData.Id;
        }
        public DriverServiceModel GetDriverForTrip(int tripId)
            => this.data.Trips
            .Where(t => t.Id == tripId)
            .Select(t => new DriverServiceModel
            {
                Name = t.Driver.Name,
                PhoneNumber = t.Driver.PhoneNumber,
                ProfilePictureUrl = t.Driver.ProfilePictureUrl,
                TimesDriver = t.Driver.TimesDriver,
                AvrgRating = AverageRating(t.Driver.Feedbacks.Select(f => f.Rating).ToList()),
                Feedbacks = t.Driver.Feedbacks
                .Select(f => new FeedbackServiceModel
                {
                    Rating = f.Rating,
                    Comment = f.Comment
                })
            })
            .FirstOrDefault();

        public List<DriverServiceModel> GetTopDrivers()
        {
            var drivers = this.data.Drivers
                .Select(d => new DriverServiceModel
                {
                    Name = d.Name,
                    ProfilePictureUrl = d.ProfilePictureUrl,
                    AvrgRating = AverageRating(d.Feedbacks.Select(d => d.Rating).ToList()),
                    Feedbacks = d.Feedbacks
                        .Select(f => new FeedbackServiceModel
                            {
                                Comment = f.Comment,
                                Rating = f.Rating
                            })
                })
                .ToList();

            return drivers.OrderByDescending(d => d.AvrgRating)
                .Take(2)
                .ToList();
        }
            

        public int GetIdByUser(string userId)
            => this.data.Drivers
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();

        public int GetIdByTrip(int tripId)
        {
            var trips1 = this.data.Trips
                .Where(t => t.Id == tripId);
            
            var id = trips1.Select(t => t.DriverId)
                .FirstOrDefault();

            return id;
        }

        public int GetIdByTrip2(int tripId)
            => this.data.Drivers
                .Where(d => d.Trips.Any(t => t.Id == tripId))
                .FirstOrDefault()
                .Id;

        public bool UserIsDriver(string userId)
        => this.data.Drivers
                .Any(d => d.UserId == userId);

        private static double AverageRating(List<int> rates)
        {
            var count = rates.Count();
            var sum = rates.Sum();

            if(count == 0)
            {
                return 0;
            }

            return (double)sum / count;
        }
    }
}
