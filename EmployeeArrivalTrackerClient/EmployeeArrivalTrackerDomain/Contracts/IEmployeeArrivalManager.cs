using Common.Models.Employees;
using System.Collections.Generic;

namespace EmployeeArrivalTrackerDomain.Contracts
{
    public interface IEmployeeArrivalManager
    {
        List<EmployeesVM> GetAllArrivalEmployees();

        void AddArrivalAmployees(object data, string token);
    }
}
