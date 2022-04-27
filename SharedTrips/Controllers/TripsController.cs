using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedTrips.Data.Models;
using SharedTrips.Extensions;
using SharedTrips.Models.Trips;
using SharedTrips.Services.Cars;
using SharedTrips.Services.Drivers;
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
        private readonly ITripsService trips;

        private readonly IDriversService drivers;

        private readonly ICarsService cars;

        public TripsController(ITripsService trips, IDriversService drivers, ICarsService cars)
        {
            this.trips = trips;
            this.drivers = drivers;
            this.cars = cars;
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!drivers.UserIsDriver(this.User.GetId()))
            {
                return RedirectToAction(nameof(DriversController.Become), "Drivers");
            }

            return View(new AddTripFormModel
            {
                Cities = this.trips.GetCities(),
                Cars = cars.GetCarsForDriver(this.drivers.GetIdByUser(this.User.GetId()))
            }); 

        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddTripFormModel trip)
        {
            if (!drivers.UserIsDriver(this.User.GetId()))
            {
                return RedirectToAction(nameof(DriversController.Become), "Drivers");
            }

            ValidateTripFormModel(trip);

            if (!ModelState.IsValid)
            {
                trip.Cities = this.trips.GetCities();
                trip.Cars = cars.GetCarsForDriver(this.drivers.GetIdByUser(this.User.GetId()));
                return View(trip);
            }

            trips.AddTrip(
                trip.MaxPassengers,
                trip.TimeOfDeparture,
                trip.Price,
                trip.FromCityId,
                trip.ToCityId,
                drivers.GetIdByUser(this.User.GetId()),
                trip.CarId);

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllTripsViewModel query)
        {
            var tripsService = this.trips.GetTrips(query.FromCityId, query.ToCityId, query.TimeOfDeparture);

            query.TotalTrips = tripsService.TotalTrips;

            query.Cities = tripsService.Cities;

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
                DriverRating = t.DriverRating,
                CarName = this.cars.GetName(t.CarId)
            }).ToList();

            return View(query);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var userIsDriver = this.drivers.GetIdByTrip(id) == this.drivers.GetIdByUser(this.User.GetId());
            var tripDetails = this.trips.GetTripDetails(id);
            var driverDetails = this.drivers.GetDriverForTrip(id);
            var carDetails = this.cars.GetCarForTrip(id);

            var details = new DetailsTripViewModel
            {
                Id = id,
                UserIsDriver = userIsDriver,
                Trip = tripDetails,
                Driver = driverDetails,
                Car = carDetails
            };

            return View(details);
       }

        private void ValidateTripFormModel(AddTripFormModel trip)
        {
            if (trip.FromCityId == trip.ToCityId)
            {
                this.ModelState.AddModelError(nameof(trip.FromCityId),
                    "You must enter 2 different cities.");
            }

            if (!trips.GetCities().Any(c => c.Id == trip.FromCityId)
                || !trips.GetCities().Any(c => c.Id == trip.ToCityId))
            {
                this.ModelState.AddModelError(nameof(trip.FromCityId),
                    "City not found");
            }
        }
    }
}
