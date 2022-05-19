using Common.Models.Employees;
using Common.Pagination;
using EmployeeArrivalTrackerDataAccess.Data;
using System;
using System.Collections.Generic;

namespace EmployeeArrivalTrackerDataAccess.Contracts
{
    public interface IEmployeeArrivalDbManager
    {
        void AddArrivalEmployees(List<EmployeeArrival> empList);

        PagedResult<EmployeesVM> GetAllArrivalEmployeesBySpecificDate(DateTime currentDate, int p);
    }
}
