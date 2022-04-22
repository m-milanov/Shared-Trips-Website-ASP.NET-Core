using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedTrips.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrips.Data
{
    public class SharedTripsDbContext : IdentityDbContext
    {

        public DbSet<Trip> Trips { get; set; } 

        public DbSet<City> Cities { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

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
                .HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Driver>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
