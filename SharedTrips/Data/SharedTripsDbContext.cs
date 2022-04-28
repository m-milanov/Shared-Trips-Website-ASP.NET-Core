using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedTrips.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrips.Data
{
    public class SharedTripsDbContext : IdentityDbContext<Passenger>
    {

        public DbSet<Trip> Trips { get; set; } 

        public DbSet<City> Cities { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<TripPassenger> TripPassenger { get; set; }

        public SharedTripsDbContext(DbContextOptions<SharedTripsDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Trip>()
                .HasOne(t => t.FromCity)
                .WithMany(c => c.StartOfTrip)
                .HasForeignKey(t => t.FromCityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Trip>()
                .HasOne(t => t.ToCity)
                .WithMany(c => c.EndOfTrip)
                .HasForeignKey(t => t.ToCityId);

            builder.Entity<Driver>()
                .HasOne<Passenger>()
                .WithOne()
                .HasForeignKey<Driver>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Trip>()
                .HasOne(t => t.Car)
                .WithMany(c => c.Trips)
                .HasForeignKey(t => t.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Trip>()
                .HasOne(t => t.Driver)
                .WithMany(d => d.Trips)
                .HasForeignKey(t => t.DriverId)
                .OnDelete(DeleteBehavior.Restrict);
            /*
            builder.Entity<Trip>()
                .HasMany(t => t.Passengers)
                .WithMany(p => p.Trips)
                .UsingEntity(j => j.ToTable("TripPassengers"));

            builder.Entity<Trip>()
                .HasMany(t => t.RequestedPassengers)
                .WithMany(p => p.RequestedTrips)
                .UsingEntity(j => j.ToTable("TripPassengersRequest"));
            */

            builder.Entity<TripPassenger>()
                .HasKey(tp => new { tp.TripId, tp.PassengerId });

            builder.Entity<TripPassenger>()
                .HasOne(tp => tp.Trip)
                .WithMany(t => t.TripPassengers)
                .HasForeignKey(tp => tp.TripId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TripPassenger>()
                .HasOne(tp => tp.Passenger)
                .WithMany(p => p.TripPassengers)
                .HasForeignKey(tp => tp.PassengerId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
