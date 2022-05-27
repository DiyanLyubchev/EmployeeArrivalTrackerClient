using Common.Models.Employees;
using Common.Models.Producer;
using System.Collections.Generic;

namespace EmployeeArrivalTrackerDomain.Contracts
{
    public interface IEmployeeArrivalManager
    {
        List<EmployeesVM> GetAllArrivalEmployees();

        bool AddArrivalEmployees(List<ProducerArrivalEmployeesVM> data, string token);
    }
}
