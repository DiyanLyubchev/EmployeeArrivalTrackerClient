using EmployeeArrivalTrackerDomain.Models;
using System.Collections.Generic;

namespace EmployeeArrivalTrackerDomain.Contracts
{
    public interface IEmployeeArrivalManager
    {
        List<ArrivalEmployeeVM> GetAllArrivalEmployees();

        bool AddArrivalAmployees(object data, string token);
    }
}
