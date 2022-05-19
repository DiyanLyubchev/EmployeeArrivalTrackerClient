using Common.Models.Employees;
using EmployeeArrivalTrackerDataAccess.Data;
using System;
using System.Collections.Generic;

namespace EmployeeArrivalTrackerDataAccess.Contracts
{
    public interface IEmployeeArrivalDbManager
    {
        void AddArrivalEmployees(List<EmployeeArrival> empList);

        List<EmployeesVM> GetAllArrivalEmployeesBySpecificDate(DateTime currentDate);
    }
}
