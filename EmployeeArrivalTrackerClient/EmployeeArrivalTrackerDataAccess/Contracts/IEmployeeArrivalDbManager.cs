using EmployeeArrivalTrackerDataAccess.Data;
using System;
using System.Collections.Generic;

namespace EmployeeArrivalTrackerDataAccess.Contracts
{
    public interface IEmployeeArrivalDbManager
    {
        void AddArrivalEmployees(List<EmployeeArrivalTable> empList);

        List<EmployeeArrivalTable> GetAllArrivalEmployeesBySpecificDate(DateTime currentDate);
    }
}
