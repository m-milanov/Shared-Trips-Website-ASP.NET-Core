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

        public int GetIdByUser(string userId)
            => this.data
                .Drivers
                .Where(d => d.UserId == userId)
                .Select(d => d.Id)
                .FirstOrDefault();

        public bool UserIsDriver(string userId)
        => this.data.Drivers
                .Any(d => d.UserId == userId);



    }
}
