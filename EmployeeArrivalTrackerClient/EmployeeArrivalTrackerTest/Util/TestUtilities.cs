using EmployeeArrivalTrackerDataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace EmployeeArrivalTrackerTest.Util
{
    public static class TestUtilities
    {
        public static DbContextOptions<EmployeeArrivalContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<EmployeeArrivalContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }
    }
}
