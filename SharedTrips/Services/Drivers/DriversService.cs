using SharedTrips.Data;
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
