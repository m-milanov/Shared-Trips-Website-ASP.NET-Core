using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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
        public const string AdministratorRoleName = "Administrator";
        private const string adminEmail = "admin@gmail.com";
        private const string adminName = "Admin";
        private const string adminPassword = "123456";
        private const string usersPassword = "123456";

        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var services = scopedServices.ServiceProvider;

            //data.Database.Migrate();

            SeedCities(services);
            SeedUsers(services);
            SeedAdministrator(services);

            return app;
        }

        private static void SeedCities(IServiceProvider services)
        {
            var data = services.GetService<SharedTripsDbContext>();


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

        private static void SeedUsers(IServiceProvider services)
        {
            var userManeger = services.GetRequiredService<UserManager<Passenger>>();
            var data = services.GetService<SharedTripsDbContext>();

            if (data.Users.Any())
            {
                return;
            }

            Task
                .Run(async () =>
                {
                    var users = new List<Passenger>
                    {
                        new Passenger{
                            Email = "user1@gmail.com",
                            UserName = "user1@gmail.com",
                            FullName = "Martin Milanov",
                            Age = 19,
                        },
                        new Passenger{
                            Email = "user2@gmail.com",
                            UserName = "user2@gmail.com",
                            FullName = "Zhuliet Yaneva",
                            Age = 19,
                        },
                        new Passenger{
                            Email = "user3@gmail.com",
                            UserName = "user3@gmail.com",
                            FullName = "Ivaylo Milanov",
                            Age = 25,
                        },
                        new Passenger{
                            Email = "user4@gmail.com",
                            UserName = "user4@gmail.com",
                            FullName = "Ivan Dekov",
                            Age = 19,
                        },
                    };

                    foreach(var user in users)
                    {
                        await userManeger.CreateAsync(user, usersPassword);
                    }
                    
                })
                .GetAwaiter()
                .GetResult();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManeger = services.GetRequiredService<UserManager<Passenger>>();
            var roleManeger = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if(await roleManeger.RoleExistsAsync(AdministratorRoleName))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = AdministratorRoleName };

                    await roleManeger.CreateAsync(role);

                    var user = new Passenger
                    {
                        Email = adminEmail,
                        UserName = adminName,
                        FullName = adminName,
                        Age = 20,
                    };

                    await userManeger.CreateAsync(user, adminPassword);

                    await userManeger.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }

    }
}
