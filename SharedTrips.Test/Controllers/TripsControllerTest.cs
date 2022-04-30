using Microsoft.AspNetCore.Mvc;
using SharedTrips.Controllers;
using SharedTrips.Data;
using SharedTrips.Data.Models;
using SharedTrips.Models.Trips;
using SharedTrips.Services.Cars;
using SharedTrips.Services.Drivers;
using SharedTrips.Services.Trips;
using SharedTrips.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SharedTrips.Test.Controllers
{
    public class TripsControllerTest
    {
        [Fact]
        public void AllTripsShouldReturnView()
        {
            //Arrange
            var data = DatabaseMock.Instance;

            var tripsService = new TripsService(data);
            var driversService = new DriversService(data);
            var carsService = new CarsService(data);

            data.Trips.AddRange(Enumerable.Range(0, 10).Select(i => new Trip()).ToList());
            data.SaveChanges();

            var tripsController = new TripsController(tripsService, driversService, carsService);

            var allTripsViewModel = new AllTripsViewModel
            {
                Cities = tripsService.GetCities(),
                FromCityId = 0,
                ToCityId = 0,
                TimeOfDeparture = DateTime.UtcNow.AddDays(-2),
            };

            //Act
            var result = tripsController.All(allTripsViewModel);

            //Assert
            Assert.NotNull(result);

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = viewResult.Model;

            var indexViewModel = Assert.IsType<AllTripsViewModel>(model);
        }

        private void AddTrips(SharedTripsDbContext data)
        {
            var trips = new List<Trip>
            {
                new Trip{
                    FromCityId = 1,
                    ToCityId = 2,
                    TimeOfDeparture = DateTime.UtcNow.AddDays(1),
                    CarId = 1,
                    MaxPassengers = 3,

                }
            };
        }
    }
}
