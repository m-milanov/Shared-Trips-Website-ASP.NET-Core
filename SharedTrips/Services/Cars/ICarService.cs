using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Services.Cars
{
    public interface ICarsService
    {
        public int Add(string brand, string model, int year, string imgUrl, int driverId);

        public string GetName(int carId);

        public List<CarServiceModel> GetCarsForDriver(int driverId);

        public CarServiceModel GetCarForTrip(int tripId);

        public CarServiceModel GetCar(int carId);

        public bool UpdateCar(int id, string brand, string model, int year, string imgUrl);
    }
}
