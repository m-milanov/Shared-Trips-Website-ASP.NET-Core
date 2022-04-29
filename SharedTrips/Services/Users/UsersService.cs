using SharedTrips.Data;
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

        
    }
}
