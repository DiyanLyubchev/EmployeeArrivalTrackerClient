using EmployeeArrivalTrackerDataAccess.Context;
using EmployeeArrivalTrackerDataAccess.Contracts;
using EmployeeArrivalTrackerDataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeArrivalTrackerDataAccess.DbManager
{
    public class EmployeeArrivalDbManager : IEmployeeArrivalDbManager
    {
        private EmployeeArrivalContext context;

        public EmployeeArrivalDbManager(EmployeeArrivalContext context)
        {
            this.context = context;
        }

        public void AddArrivalEmployees(List<EmployeeArrivalTable> empList)
        {
            this.context.EmployeeArrivalTable.AddRange(empList);
            this.context.SaveChanges();
        }

        public List<EmployeeArrivalTable> GetAllArrivalEmployeesBySpecificDate(DateTime currentDate)
        {
            return this.context.EmployeeArrivalTable.Where(x => x.When.Date == currentDate.Date).ToList();
        }
    }
}
