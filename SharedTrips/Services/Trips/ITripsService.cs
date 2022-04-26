using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Services.Trips
{
    public interface ITripsService
    {
        public AllTripsServiceModel GetTrips(int fromCityId, int toCityId, DateTime timeOfDeparture);

        public int AddTrip(int maxPassengers, DateTime timeOfDeparture,
            int price, int fromcityId, int toCityId, int driverId, int carId);

        public IEnumerable<CityServiceModel> GetCities(); 
    }
}
