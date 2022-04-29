using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharedTrips.Services.Users
{
    public interface IUsersService
    {
        public IEnumerable<RateDriverServiceModel> GetDriversToRate(string userId);
    }
}
