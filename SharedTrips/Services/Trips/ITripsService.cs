using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Services.Trips
{
    public interface ITripsService
    {
        public AllTripsServiceModel GetTrips(int fromCityId, int toCityId, DateTime timeOfDeparture);

        public List<TripServiceListingModel> GetTripsAsPassenger(string userId);

        public List<TripServiceListingModel> GetTripsAsDriver(int driverId);

        public TripServiceModel GetTrip(int tripId);

        public DetailsTripServiceModel GetTripDetails(int tripId);

        public int AddTrip(int maxPassengers, DateTime timeOfDeparture,
            int price, int fromcityId, int toCityId, int driverId, int carId);

        public void UpdateTrip(int id, TripServiceModel trip);

        public void DeleteTrip(int id);

        public void EndTrip(int id);

        public void UserRequest(int tripId, string userId);

        public void AcceptRequest(int tripId, string userId);

        public void RemoveUser(int tripId, string userId);

        public bool UserIsDriver(int tripId, string userId);

        public IEnumerable<CityServiceModel> GetCities();

        public IEnumerable<PassengerServiceModel> GetPassengers(int tripId);
    }
}
