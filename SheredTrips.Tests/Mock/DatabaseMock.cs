using Microsoft.EntityFrameworkCore;
using SharedTrips.Data;
using System;

namespace SheredTrips.Tests.Mock
{
    public static class DatabaseMock
    {
        public static SharedTripsDbContext Instance
        {
            get{
                var dbContextOptions = new DbContextOptionsBuilder<SharedTripsDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());

                return new SharedTripsDbContest()
            }
        }
    }
}
