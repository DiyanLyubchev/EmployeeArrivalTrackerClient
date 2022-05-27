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

        public List<EmployeeReport> GetAllArrivalEmployeesBySpecificDate()
        {
            string query = @$"select e.id, e.name, e.SurName, e.age, e.email,e.ManagerId, t.name TeamName, r.name RoleName, a.WhenArrival 
                             from dbo.Employees e 
                             join dbo.EmployeeArrivals a on e.Id = a.EmployeeId
                             join dbo.Roles r on e.RolesNomenclatureId = r.Id
                             join dbo.EmployeeTeamsNomenclatures en on e.Id = en.EmployeeId
                             join dbo.Teams t on en.TeamsNomenclatureId = t.Id";

            var dataVm = this.context.EmployeeReport
                .FromSqlRaw(query)
                .AsNoTracking()
                .OrderByDescending(x => x.WhenArrival)
                .ToList();

            //List<EmployeesVM> dataVm = this.context.Employees
            //     .Include(x => x.EmployeeArrival)
            //     .Include(x => x.RolesNomenclature)
            //     .Include(x => x.EmployeeTeamsNomenclatures)
            //     .Where(x =>
            //           x.RolesNomenclature.Id == x.RolesNomenclatureId &&
            //           x.EmployeeArrival.EmployeeId == x.Id)
            //     .Select(x => new EmployeesVM
            //     {
            //         Id = x.Id,
            //         Name = x.Name,
            //         SurName = x.SurName,
            //         Age = x.Age,
            //         Email = x.Email,
            //         ManagerId = x.ManagerId,
            //         Role = x.RolesNomenclature.Name,
            //         When = x.EmployeeArrival.WhenArrival
            //     })
            //     .OrderByDescending(x => x.When)
            //     .ToList();

            return dataVm;
        }
    }
}
