using Microsoft.EntityFrameworkCore;
using SharedTrips.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedTrips.Test.Mocks
{
    public static class DatabaseMock
    {
        public static SharedTripsDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<SharedTripsDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new SharedTripsDbContext(dbContextOptions);
            }
        }
    }
}
