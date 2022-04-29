using SharedTrips.Data;
using SharedTrips.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly SharedTripsDbContext data;

        public UsersService(SharedTripsDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<RateDriverServiceModel> GetDriversToRate(string userId)
            => this.data.PassengerDrivers
                .Where(pd => pd.PassengerId == userId)
                .Select(pd => new RateDriverServiceModel
                {
                    Id = pd.DriverId,
                    Name = pd.Driver.Name,
                    ProfilePictureUrl = pd.Driver.ProfilePictureUrl,
                })
                .ToList();
        public void RateDriver(int driverId, string userId, RateDriverServiceModel driver)
        {
            var feedback = new Feedback
            {
                Rating = driver.Rating,
                Comment = driver.Comment,
                DriverId = driverId
            };

            this.data.Feedbacks.Add(feedback);

            var pd = this.data.PassengerDrivers
                .Where(pd => pd.PassengerId == userId && pd.DriverId == driverId)
                .FirstOrDefault();

            this.data.PassengerDrivers.Remove(pd);

            this.data.SaveChanges();
        }
        
    }
}
