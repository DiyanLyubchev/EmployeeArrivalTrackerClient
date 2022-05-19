using Common.Models.Producer;
using EmployeeArrivalTrackerDataAccess.Data;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeArrivalTrackerDomain.Adapter
{
    public static class EmployeeAdapter
    {
        public static List<EmployeeArrival> Transform(List<ProducerArrivalEmployeesVM> request)
        {
            return request
                .Select(x =>
                new EmployeeArrival(x.EmployeeId,x.When))
                .ToList();
        }
    }
}
