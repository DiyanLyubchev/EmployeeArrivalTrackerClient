using Common.Models.Employees;
using EmployeeArrivalTrackerDataAccess.Context;
using EmployeeArrivalTrackerDataAccess.Contracts;
using EmployeeArrivalTrackerDataAccess.Data;
using Microsoft.EntityFrameworkCore;
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

        public void AddArrivalEmployees(List<EmployeeArrival> empList)
        {
            this.context.EmployeeArrivals.AddRange(empList);
            this.context.SaveChanges();
        }

        public List<EmployeesVM> GetAllArrivalEmployeesBySpecificDate(DateTime currentDate)
        {
            var dataVm = this.context.Employees
                 .Include(x => x.EmployeeArrival)
                 .Include(x => x.RolesNomenclature)
                 .Include(x => x.EmployeeTeamsNomenclatures)
                 .Where(x =>
                       x.EmployeeArrival.When.Date == currentDate.Date &&
                       x.RolesNomenclature.Id == x.RolesNomenclatureId &&
                       //  x.EmployeeTeamsNomenclatures.All(r => r.EmployeesTableId == x.Id) &&
                       x.EmployeeArrival.EmployeeId == x.Id)
                 .Select(x => new EmployeesVM
                 {
                     Id = x.Id,
                     Name = x.Name,
                     SurName = x.SurName,
                     Age = x.Age,
                     Email = x.Email,
                     ManagerId = x.ManagerId,
                     Role = x.RolesNomenclature.Name,
                     When = x.EmployeeArrival.When
                 })
                 .ToList();

            return dataVm;
        }
    }
}
