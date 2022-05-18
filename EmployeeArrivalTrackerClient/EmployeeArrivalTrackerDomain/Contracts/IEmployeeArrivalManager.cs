using EmployeeArrivalTrackerDomain.Models;
using System.Collections.Generic;

namespace EmployeeArrivalTrackerDomain.Contracts
{
    public interface IEmployeeArrivalManager
    {
        List<ArrivalEmployeeVM> GetAllArrivalEmployees();

        void AddArrivalAmployees(object data, string token);
    }
}
