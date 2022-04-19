using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedTrips.Data;
using SharedTrips.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<SharedTripsDbContext>();

            //data.Database.Migrate();

            data.Database.EnsureCreated();

            SeedCities(data);

            return app;
        }

        private static void SeedCities(SharedTripsDbContext data)
        {
            if (data.Cities.Any())
            {
                return;
            }

            data.Cities.AddRange(new[]
            {
                new City {Name = "Sofia"},
                new City {Name = "Stara Zagora"},
                new City {Name = "Plovdiv"},
                new City {Name = "Varna"},
                new City {Name = "Burgas"},
                new City {Name = "Ruse"},
                new City {Name = "Sliven"},
            });

            data.SaveChanges();
        }
    }
}
