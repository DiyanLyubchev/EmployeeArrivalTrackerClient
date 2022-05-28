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

        public List<EmployeeReport> GetAllArrivalEmployeesByQuery()
        {
            string query = @$"select 
                              e.Id,
                              e.Name, 
                              e.SurName, 
                              e.Age, 
                              e.Email, 
                              e.ManagerId, 
                              t.Name TeamName,
                              r.Name RoleName,
                              a.WhenArrival 
                              from dbo.Employees e 
                              join dbo.EmployeeArrivals a on e.Id = a.EmployeeId
                              join dbo.Roles r on e.RolesNomenclatureId = r.Id
                              join dbo.EmployeeTeamsNomenclatures en on e.Id = en.EmployeeId
                              join dbo.Teams t on en.TeamsNomenclatureId = t.Id";

            var dataVm = this.context.EmployeeReport
                .FromSqlRaw(query)
                .AsNoTracking()
                .ToList();

            return dataVm;
        }

        public List<EmployeesVM> GetAllArrivalEmployeesByEF()
        {
            List<EmployeesVM> dataVm = (from employee in this.context.Employees
                                        join arrival in this.context.EmployeeArrivals
                                        on employee.Id equals arrival.EmployeeId
                                        join roles in this.context.Roles
                                        on employee.RolesNomenclatureId equals roles.Id
                                        join empTeam in this.context.EmployeeTeamsNomenclatures
                                        on employee.Id equals empTeam.EmployeeId
                                        join teams in this.context.Teams
                                        on empTeam.TeamsNomenclatureId equals teams.Id
                                        select new EmployeesVM
                                        {
                                            Id = employee.Id,
                                            Name = employee.Name,
                                            SurName = employee.SurName,
                                            Age = employee.Age,
                                            Email = employee.Email,
                                            ManagerId = employee.ManagerId,
                                            Role = roles.Name,
                                            TeamDescription = teams.Name,
                                            WhenArrival = arrival.WhenArrival
                                        })
                                        .ToList();

            return dataVm;
        }
    }
}
