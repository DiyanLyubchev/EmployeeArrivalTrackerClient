using EmployeeArrivalTrackerDataAccess.Data;
using System;
using System.Collections.Generic;

namespace EmployeeArrivalTrackerDataAccess.Contracts
{
    public interface IEmployeeArrivalDbManager
    {
        void AddArrivalEmployees(List<EmployeeArrival> empList);

        List<EmployeeArrival> GetAllArrivalEmployeesBySpecificDate(DateTime currentDate);
    }
}
