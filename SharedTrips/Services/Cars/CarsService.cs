using SharedTrips.Data;
using SharedTrips.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Services.Cars
{
    public class CarsService : ICarsService
    {
        private readonly SharedTripsDbContext data;

        public CarsService(SharedTripsDbContext data)
        {
            this.data = data;
        }

        public int Add(string brand, string model, int year, string imgUrl, int driverId) 
        {
            var car = new Car
            {
                Brand = brand,
                Model = model,
                Year = year,
                ImgUrl = imgUrl,
                DriverId = driverId,
            };

            data.Cars.Add(car);
            data.SaveChanges();

            return car.Id;
        }

        public string GetName(int carId)
            => data.Cars
            .Where(c => c.Id == carId)
            .Select(c => c.Brand + " " + c.Model + " " + c.Year)
            .First();

        public CarServiceModel GetCar(int carId)
            => this.data.Cars
                .Where(c => c.Id == carId)
                .Select(c => new CarServiceModel
                {
                    Id = c.Id,
                    Brand = c.Brand,
                    Model = c.Model,
                    Year = c.Year,
                    ImgUrl = c.ImgUrl,
                    UserId = c.Driver.UserId
                })
                .First();
        public bool UpdateCar(int id, string brand, string model, int year, string imgUrl)
        {
            var car = this.data.Cars.Where(c => c.Id == id).FirstOrDefault();

            if(car == null)
            {
                return false;
            }

            car.Brand = brand;
            car.Model = model;
            car.Year = year;
            car.ImgUrl = imgUrl;

            this.data.SaveChanges();

            return true;
        }

        public List<CarServiceModel> GetCarsForDriver(int driverId)
            => data.Cars
            .Where(c => c.DriverId == driverId)
            .Select(c => new CarServiceModel
            {
                Id = c.Id,
                Brand = c.Brand,
                Model = c.Model,
                Year = c.Year,
                ImgUrl = c.ImgUrl
            })
            .ToList();

        public CarServiceModel GetCarForTrip(int tripId)
            => data.Trips
            .Where(t => t.Id == tripId)
            .Select(t => new CarServiceModel
            {
                Brand = t.Car.Brand,
                Model = t.Car.Model,
                Year = t.Car.Year,
                ImgUrl = t.Car.ImgUrl
            })
            .FirstOrDefault();

        
    }
}
