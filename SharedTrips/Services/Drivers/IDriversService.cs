using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Services.Drivers
{
    public interface IDriversService
    {
        public bool UserIsDriver (string userId);

        public int GetIdByUser(string userId);

        public int GetIdByTrip(int tripId);

        public DriverServiceModel GetDriverForTrip(int tripId);

        public int AddDriver(string name, string phoneNumber, string profilePictureUrl, string userId);
        
    }
}
