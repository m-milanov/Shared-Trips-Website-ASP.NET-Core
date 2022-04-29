using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

            return View(new TripServiceModel
            {
                Cities = this.trips.GetCities(),
                Cars = cars.GetCarsForDriver(this.drivers.GetIdByUser(this.User.GetId()))
            }); 

        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(TripServiceModel trip)
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

            ViewBag.Editing = true;

            return RedirectToAction(nameof(All));
        }

        public IActionResult All([FromQuery] AllTripsViewModel query)
        {
            var tripsService = this.trips.GetTrips(query.FromCityId, query.ToCityId, query.TimeOfDeparture);

            query.TotalTrips = tripsService.TotalTrips;

            query.Cities = tripsService.Cities;

            query.Trips = MapTripListingViewModel(tripsService.Trips);

            return View(query);
        }

        [Authorize]
        public IActionResult UserTrips()
        {
            var tripsView = new UserTripsViewModel();

            var tripsAsPassenger = MapTripListingViewModel(
                    this.trips.GetTripsAsPassenger(this.User.GetId()));

            tripsView.TripsAsPassenger = tripsAsPassenger;

            if (this.drivers.UserIsDriver(this.User.GetId()))
            {
                var tripsAsDriver = MapTripListingViewModel(
                    this.trips.GetTripsAsDriver(this.drivers.GetIdByUser(this.User.GetId())));

                tripsView.TripsAsDriver = tripsAsDriver;
                tripsView.UserIsDriver = true;
            }

            return View(tripsView);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var trip = this.trips.GetTrip(id);

            if (trip == null)
            {
                return BadRequest();
            }

            var userIsDriver = this.drivers.GetIdByTrip(id) == this.drivers.GetIdByUser(this.User.GetId());
            var tripDetails = this.trips.GetTripDetails(id);
            var driverDetails = this.drivers.GetDriverForTrip(id);
            var carDetails = this.cars.GetCarForTrip(id);
            var passengers = this.trips.GetPassengers(id);
            var userIsInTrip = passengers.Any(p => p.Id == this.User.GetId());

            var details = new DetailsTripViewModel
            {
                Id = id,
                UserIsDriver = userIsDriver,
                UserIsInTrip = userIsInTrip,
                Trip = tripDetails,
                Driver = driverDetails,
                Car = carDetails,
                Passengers = passengers,
            };

            return View(details);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            if(!this.trips.UserIsDriver(id, this.User.GetId()))
            {
                return Unauthorized();
            }
            
            var trip = this.trips.GetTrip(id);

            if (trip == null)
            {
                return BadRequest();
            }

            trip.Cities = this.trips.GetCities();
            trip.Cars = this.cars.GetCarsForDriver(this.drivers.GetIdByUser(this.User.GetId()));

            ViewBag.Editing = true;

            return View(trip);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, TripServiceModel trip)
        {
            if (!this.trips.UserIsDriver(id, this.User.GetId()))
            {
                return Unauthorized();
            }

            ValidateTripFormModel(trip);

            if (!ModelState.IsValid)
            {
                trip.Cities = this.trips.GetCities();
                trip.Cars = cars.GetCarsForDriver(this.drivers.GetIdByUser(this.User.GetId()));
                return View(trip);
            }

            this.trips.UpdateTrip(id, trip);

            return RedirectToAction("All", "Trips");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            if (!this.trips.UserIsDriver(id, this.User.GetId()))
            {
                return Unauthorized();
            }

            var trip = this.trips.GetTrip(id);

            if(trip == null)
            {
                return BadRequest();
            }

            this.trips.DeleteTrip(id);

            return RedirectToAction("All", "Trips");
        }

        [Authorize]
        public IActionResult End(int id)
        {
            if (!this.trips.UserIsDriver(id, this.User.GetId()))
            {
                return Unauthorized();
            }

            var trip = this.trips.GetTrip(id);

            if (trip == null)
            {
                return BadRequest();
            }

            this.trips.EndTrip(id);

            return RedirectToAction("All", "Trips");
        }

        private void ValidateTripFormModel(TripServiceModel trip)
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

        private List<TripListingViewModel> MapTripListingViewModel(List<TripServiceListingModel> trips)
          =>  trips.Select(t => new TripListingViewModel
            {
                Id = t.Id,
                Price = t.Price,
                FromCity = t.FromCity,
                ToCity = t.ToCity,
                TimeOfDeparture = t.TimeOfDeparture,
                MaxPassengers = t.MaxPassengers,
                FreeSeats = t.FreeSeats,
                DriverName = t.DriverName,
                DriverPictureUrl = t.DriverPictureUrl,
                DriverRating = t.DriverRating,
                CarName = this.cars.GetName(t.CarId)
            }).ToList();


    }
}
