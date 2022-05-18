using Common.Models.Producer;
using EmployeeArrivalTrackerDataAccess.Data;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeArrivalTrackerDomain.Adapter
{
    public static class EmployeeAdapter
    {
        public static List<EmployeeArrivalTable> Transform(List<ProducerArrivalEmployeesVM> request)
        {
            return request
                .Select(x =>
                new EmployeeArrivalTable(x.EmployeeId,x.When))
                .ToList();
        }
    }
}
