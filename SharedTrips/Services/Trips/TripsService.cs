using SharedTrips.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                    FromCity = t.FromCity.Name,
                    ToCity = t.ToCity.Name,
                    DriverName = t.Driver.Name,
                    DriverPictureUrl = t.Driver.ProfilePictureUrl,
                    DriverRating = t.Driver.Feedbacks.Count() == 0 ? 0 : t.Driver.Feedbacks.Select(f => f.Rating).Sum() / t.Driver.Feedbacks.Count()
                }).ToList();

            query.Cities = this.GetCities();
            query.TotalTrips = totalTrips;

            return query;
        }

        private IEnumerable<CityServiceModel> GetCities()
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
