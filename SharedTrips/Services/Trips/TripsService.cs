using SharedTrips.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedTrips.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace SharedTrips.Services.Trips
{
    public class TripsService : ITripsService
    {
        private readonly SharedTripsDbContext data;

        public TripsService(SharedTripsDbContext data)
            => this.data = data;

        public AllTripsServiceModel GetTrips(int fromCityId, int toCityId, DateTime timeOfDeparture)
        {
            var query = new AllTripsServiceModel();

            var tripsQuery = this.data.Trips.AsQueryable();

            if (fromCityId != 0 && toCityId != 0 && timeOfDeparture > DateTime.UtcNow)
            {
                tripsQuery = tripsQuery.Where(t =>
                t.FromCityId == fromCityId &&
                t.ToCityId == toCityId &&
                t.TimeOfDeparture.Date == timeOfDeparture.Date);
            }

            var totalTrips = tripsQuery.Count();

            query.Trips = tripsQuery
                //.Skip((query.CurrentPage - 1) * query.TripsPerPage)
                //.Take(query.TripsPerPage)
                .OrderByDescending(t => t.Id)
                .Select(t => new TripServiceListingModel
                {
                    Id = t.Id,
                    Price = t.Price,
                    TimeOfDeparture = t.TimeOfDeparture,
                    MaxPassengers = t.MaxPassengers,
                    FreeSeats = t.MaxPassengers - t.TripPassengers.Where(tp => tp.Accepted).Count(),
                    FromCity = t.FromCity.Name,
                    ToCity = t.ToCity.Name,
                    DriverName = t.Driver.Name,
                    DriverPictureUrl = t.Driver.ProfilePictureUrl,
                    DriverRating = t.Driver.Feedbacks.Count() == 0 ? 0 : t.Driver.Feedbacks.Select(f => f.Rating).Sum() / t.Driver.Feedbacks.Count(),
                    CarId = t.Car.Id
                }).ToList();

            query.Cities = this.GetCities();
            query.TotalTrips = totalTrips;

            return query;
        }

        public TripServiceModel GetTrip(int tripId)
            => this.data.Trips
            .Where(t => t.Id == tripId)
            .Select(t => new TripServiceModel
            {
                Price = t.Price,
                TimeOfDeparture = t.TimeOfDeparture,
                MaxPassengers = t.MaxPassengers,
                FromCityId = t.FromCityId,
                ToCityId = t.ToCityId,
                CarId = t.CarId
            })
            .FirstOrDefault();

        public int AddTrip(
            int maxPassengers,
            DateTime timeOfDeparture,
            int price,
            int fromCityId,
            int toCityId,
            int driverId,
            int carId)
        {
            var trip = new Trip
            {
                MaxPassengers = maxPassengers,
                TimeOfDeparture = timeOfDeparture,
                Price = price,
                FromCityId = fromCityId,
                ToCityId = toCityId,
                DriverId = driverId,
                Car = this.data.Cars.Where(c => c.Id == carId).FirstOrDefault()
            };

            this.data.Trips.Add(trip);
            this.data.SaveChanges();

            return trip.Id;
        }

        public void UpdateTrip(int id, TripServiceModel trip)
        {
            var tripData = this.data.Trips
                .Where(t => t.Id == id)
                .FirstOrDefault();

            tripData.Price = trip.Price;
            tripData.TimeOfDeparture = trip.TimeOfDeparture;
            tripData.MaxPassengers = trip.MaxPassengers;
            tripData.FromCityId = trip.FromCityId;
            tripData.ToCityId = trip.ToCityId;
            tripData.CarId = trip.CarId;

            this.data.SaveChanges();
        }

        public void DeleteTrip(int id)
        {
            var trip = this.data.Trips
                .Where(t => t.Id == id)
                .FirstOrDefault();

            this.data.TripPassenger
                .Where(tp => tp.TripId == id)
                .ToList()
                .ForEach(tp => 
                    this.data.TripPassenger
                    .Remove(tp));

            this.data.Trips.Remove(trip);

            this.data.SaveChanges();
        }

        public void UserRequest(int tripId, string userId)
        {
            var trip = this.data.Trips
                .Where(t => t.Id == tripId)
                .FirstOrDefault();

            var user = this.data.Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            data.TripPassenger.Add(new TripPassenger
            {
                Accepted = false,
                TripId = tripId,
                Trip = trip,
                PassengerId = userId,
                Passenger = user,
            });

            this.data.SaveChanges();
        }

        public void AcceptRequest(int tripId, string userId)
        {
            var passengerRequest = this.data.TripPassenger
                .Where(tp => tp.TripId == tripId && tp.PassengerId == userId)
                .FirstOrDefault();

            passengerRequest.Accepted = true;

            data.SaveChanges();
        }

        public void RemoveUser(int tripId, string userId)
        {
            var passengerRequest = this.data.TripPassenger
                .Where(tp => tp.TripId == tripId && tp.PassengerId == userId)
                .FirstOrDefault();

            this.data.TripPassenger.Remove(passengerRequest);

            data.SaveChanges();
        }

        public bool UserIsDriver(int tripId, string userId)
            => this.data.Trips
                .Where(t => t.Id == tripId)
                .Select(t => t.Driver.UserId)
                .FirstOrDefault() == userId;

        public IEnumerable<PassengerServiceModel> GetPassengers(int tripId)
            => this.data.TripPassenger
                .Where(tp => tp.TripId == tripId)
                .Select(tp => new PassengerServiceModel
                {
                    Id = tp.PassengerId,
                    FullName = tp.Passenger.FullName,
                    Age = tp.Passenger.Age,
                    Accepted = tp.Accepted
                })
                .ToList();

        public DetailsTripServiceModel GetTripDetails(int tripId)
            => this.data.Trips
            .Where(t => t.Id == tripId)
            .Select(t => new DetailsTripServiceModel
            {
                Price = t.Price,
                MaxPassengers = t.MaxPassengers,
                FreeSeats = t.MaxPassengers - t.TripPassengers.Where(tp => tp.Accepted).Count(),
                TimeOfDeparture = t.TimeOfDeparture,
                FromCityName = t.FromCity.Name,
                ToCityName = t.ToCity.Name
            })
            .FirstOrDefault();

        public IEnumerable<CityServiceModel> GetCities()
            => this.data.Cities
            .OrderBy(c => c.Name)
            .Select(c => new CityServiceModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();

        
    }
}
