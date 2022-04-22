using Microsoft.AspNetCore.Mvc;
using SharedTrips.Data;
using SharedTrips.Data.Models;
using SharedTrips.Models.Cities;
using SharedTrips.Models.Trips;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Controllers
{
    public class TripsController : Controller
    {
        private readonly SharedTripsDbContext data;

        public TripsController(SharedTripsDbContext data)
        {
            this.data = data;
        }

        public IActionResult Add()
        {
            return View(new AddTripFormModel
            {
                Cities = this.GetCities()
            });

        }
        
        [HttpPost]
        public IActionResult Add(AddTripFormModel trip)
        {
            ValidateTripFormModel(trip);

            if (!ModelState.IsValid)
            {
                trip.Cities = this.GetCities();
                return View(trip);

            }

            var dataTrip = new Trip
            {
                Price = trip.Price,
                TimeOfDeparture = trip.TimeOfDeparture,
                MaxPassengers = trip.MaxPassengers,
                FromCityId = trip.FromCityId,
                ToCityId = trip.ToCityId,
            };

            this.data.Trips.Add(dataTrip);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery]AllTripsViewModel query)
        {
            var tripsQuery = this.data.Trips.AsQueryable();

            var q = Request.Query.ToList();

            

            if (query.FromCityId != 0 && query.ToCityId != 0 && query.TimeOfDeparture > DateTime.UtcNow)
            {

                tripsQuery = tripsQuery.Where(t =>
                t.FromCityId == query.FromCityId && 
                t.ToCityId == query.ToCityId && 
                t.TimeOfDeparture.Date == query.TimeOfDeparture.Date);
                
            }

            var totalTrips = tripsQuery.Count();

            query.Trips = tripsQuery
                //.Skip((query.CurrentPage - 1) * query.TripsPerPage)
                //.Take(query.TripsPerPage)
                .OrderByDescending(t => t.Id)
                .Select(t => new TripListingViewModel
                {
                    Id = t.Id,
                    Price = t.Price,
                    TimeOfDeparture = t.TimeOfDeparture,
                    MaxPassengers = t.MaxPassengers,
                    FromCity = t.FromCity.Name,
                    ToCity = t.ToCity.Name,
                }).ToList();

            query.Cities = this.GetCities();
            query.TotalTrips = totalTrips;
            return View(query);
        }

        private IEnumerable<CityViewModel> GetCities()
            => this.data.Cities
            .OrderBy(c => c.Name)
            .Select(c => new CityViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();

        private void ValidateTripFormModel(AddTripFormModel trip)
        {
            if (trip.FromCityId == trip.ToCityId)
            {
                this.ModelState.AddModelError(nameof(trip.FromCityId),
                    "You must enter 2 different cities.");
            }

            if (!data.Cities.Any(c => c.Id == trip.FromCityId)
                || !data.Cities.Any(c => c.Id == trip.ToCityId))
            {
                this.ModelState.AddModelError(nameof(trip.FromCityId),
                    "City not found");
            }
        }
    }


}
