﻿using Microsoft.AspNetCore.Mvc;
using SharedTrips.Data;
using SharedTrips.Data.Models;
using SharedTrips.Models.Cities;
using SharedTrips.Models.Trips;
using System;
using System.Collections.Generic;
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

            return RedirectToAction("Index", "Home");
        }

        public IActionResult All()
        {
            var trips = this.data.Trips
                .OrderByDescending(t => t.Id)
                .Select(t => new TripListingViewModel
                {
                    Id = t.Id,
                    Price = t.Price,
                    TimeOfDeparture = t.TimeOfDeparture,
                    FromCity = t.FromCity.Name,
                    ToCity = t.ToCity.Name
                }).ToList();


            return View(trips);
        }

        private IEnumerable<CityViewModel> GetCities()
            => this.data.Cities
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
