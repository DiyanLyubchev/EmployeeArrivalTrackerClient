using Common.Models.Employees;
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
                new EmployeeArrival(x.EmployeeId, x.When))
                .ToList();
        }

        public static List<EmployeesVM> Transform(List<EmployeeReport> dbData)
        {
            return dbData
                .Select(x =>
                new EmployeesVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Age = x.Age,
                    Email = x.Email,
                    ManagerId = x.ManagerId,
                    Role = x.RoleName,
                    SurName = x.SurName,
                    TeamDescription = x.TeamName,
                    WhenArrival = x.WhenArrival
                })
                .ToList();
        }
    }
}
