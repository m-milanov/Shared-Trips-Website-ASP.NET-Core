using SharedTrips.Data;
using SharedTrips.Data.Models;
using SharedTrips.Services.Drivers;
using SharedTrips.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SharedTrips.Test.Services
{
    
    public class DriversServiceTest
    {
        private const string userId = "TestUserId";
        private const string userIdFalse = "TestUserIdFalse";
        const int driverId = 123123;
        const int tripId = 123123;

        [Fact]
        public void IsDriverShouldReturnTrueWhenUserIsDriver()
        {
            //Arrange
            var data = GetDriverData();
            var driversService = new DriversService(data);

            //Act
            var result = driversService.UserIsDriver(userId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsDriverShouldReturnFalseWhenUserIsNotDriver()
        {
            //Arrange
            var data = GetDriverData();
            var driversService = new DriversService(GetDriverData());

            //Act
            var result = driversService.UserIsDriver(userIdFalse);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void AddDriverShouldAddDriverToTheDatabase()
        {
            //Arrange
            var data = DatabaseMock.Instance;
            var driversService = new DriversService(data);
            
            //Act
            var driverId = driversService.AddDriver("Ivan", "PhoneNumber", "ProfilePictureUrl",userId);
            var ressult = data.Drivers.Any(d => d.Id == driverId);

            //Assert
            Assert.True(ressult);
        }
        
        [Fact]
        public void GetIdByTripShouldReturnCorrect()
        {
            //Arrange
            var data = DatabaseMock.Instance;
            var driversService = new DriversService(data);
            var driver = new Driver { Id = driverId };
            var trip = new Trip { Id = tripId, Driver = driver, DriverId = driverId };

            //Act
            data.Drivers.Add(driver);

            data.Trips.Add(trip);

            data.SaveChanges();

            var id = driversService.GetIdByTrip(tripId);

            //Assert
            Assert.Equal(driverId, id);
               
        }
        
        [Fact]
        public void GetDriverForTripShouldReturnCorrectDriver()
        {
            //Arrange
            var data = DatabaseMock.Instance;
            var driverService = new DriversService(data);

            var driver = new Driver { Id = driverId, Name = "DriverName"};
            var trip = new Trip 
            { 
                Id = tripId,
                DriverId = driverId,
            };

            //Act
            data.Drivers.Add(driver);
            data.Trips.Add(trip);
            data.SaveChanges();


            var driverFromService = driverService.GetDriverForTrip(tripId);
            var result = driverFromService.Name == "DriverName";

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void GetIdByUserShouldReturnCorrect()
        {
            //Arrange
            var data = DatabaseMock.Instance;
            var driverService = new DriversService(data);
            var driver = new Driver { Id = driverId, Name = "DriverName", UserId = userId };

            //Act
            data.Drivers.Add(driver);
            data.SaveChanges();

            var id = driverService.GetIdByUser(userId);

            //Assert
            Assert.Equal(driverId, id);
        }

        private SharedTripsDbContext GetDriverData()
        {
            var data = DatabaseMock.Instance;

            data.Drivers.Add(new Driver { UserId = userId });
            data.SaveChanges();

            return data;
        }
    }
}
