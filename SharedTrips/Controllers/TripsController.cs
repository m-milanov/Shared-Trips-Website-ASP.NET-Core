using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedTrips.Data;
using SharedTrips.Data.Models;
using SharedTrips.Extensions;
using SharedTrips.Models.Cities;
using SharedTrips.Models.Trips;
using SharedTrips.Services.Trips;
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

        private readonly ITripsService trips;

        public TripsController(SharedTripsDbContext data, ITripsService trips)
        {
            this.data = data;
            this.trips = trips;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!UserIsDriver())
            {
                return RedirectToAction(nameof(DriversController.Become), "Drivers");
            }

            return View(new AddTripFormModel
            {
                Cities = this.GetCities()
            });

        }
        
        [HttpPost]
        [Authorize]
        public IActionResult Add(AddTripFormModel trip)
        {
            var driver = this.data
                .Drivers
                .Where(d => d.UserId == this.User.GetId())
                .FirstOrDefault();

            if (driver == null)
            {
                return RedirectToAction(nameof(DriversController.Become), "Driver");
            }

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
                DriverId = driver.Id,
            };

            this.data.Trips.Add(dataTrip);
            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery]AllTripsViewModel query)
        {
            var tripsService = this.trips.GetTrips(query.FromCityId, query.ToCityId, query.TimeOfDeparture);

            query.Trips = tripsService.Trips.Select(t => new TripListingViewModel
            {
                Id = t.Id,
                Price = t.Price,
                FromCity = t.FromCity,
                ToCity = t.ToCity,
                TimeOfDeparture = t.TimeOfDeparture,
                MaxPassengers = t.MaxPassengers,
                DriverName = t.DriverName,
                DriverPictureUrl = t.DriverPictureUrl,
                DriverRating = t.DriverRating

            }).ToList();

            query.TotalTrips = tripsService.TotalTrips;
            query.Cities = tripsService.Cities.Select(c => new CityViewModel
            {
                Id = c.Id,
                Name = c.Name
            });

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

        private bool UserIsDriver()
            => this.data.Drivers
                .Any(d => d.UserId == this.User.GetId());
    }


}
