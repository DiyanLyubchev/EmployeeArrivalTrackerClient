using EmployeeArrivalTrackerDataAccess.Data;
using EmployeeArrivalTrackerDomain.Models.Producer;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeArrivalTrackerDomain.Adapter
{
    public static class EmployeeAdapter
    {
        public static List<EmployeeArrivalTable> Transform(List<Employee> request)
        {
            return request
                .Select(x =>
                new EmployeeArrivalTable(x.EmployeeId,x.When))
                .ToList();
        }
    }
}
