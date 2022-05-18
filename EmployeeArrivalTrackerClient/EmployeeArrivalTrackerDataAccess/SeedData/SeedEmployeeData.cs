using Common.Models.Employees;
using EmployeeArrivalTrackerDataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace EmployeeArrivalTrackerDataAccess.SeedData
{
    public static class SeedEmployeeData
    {
        public static void SeedTeamsNomenclature(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamsNomenclatureTable>().HasData(TeamsNomenclature());
        }

        public static void SeedRolesNomenclature(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolesNomenclatureTable>().HasData(RolesNomenclature());
        }

        public static void SeedEmployees(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeesTable>().HasData(Employees());
        }

        public static void SeedEmployeeTeamsNomenclature(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeTeamsNomenclatureTable>().HasData(EmployeeTeamsNomenclature());
        }

        private static List<EmployeeTeamsNomenclatureTable> EmployeeTeamsNomenclature()
        {
            List<EmployeeTeamsNomenclatureTable> emploeyeeTeamsTable = new();
            List<TeamsNomenclatureTable> teams = TeamsNomenclature();

            string employeesJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataForSeed/employees.json"));
            List<EmployeesVM> data = JsonSerializer.Deserialize<List<EmployeesVM>>(employeesJson);
            int id = 1;
            foreach (var employee in data.OrderBy(x => x.Id))
            {
                int employeeId = employee.Id + 1;
                foreach (var currentTeam in employee.Teams)
                {
                    emploeyeeTeamsTable.Add(PrepareEmployeesTeamsTable(currentTeam, teams, employeeId, id));
                    id++;
                }
            }

            return emploeyeeTeamsTable;
        }

        private static EmployeeTeamsNomenclatureTable PrepareEmployeesTeamsTable(string teamName, List<TeamsNomenclatureTable> teams, int employeeId, int id)
        {
            var teamTabel = teams.First(x => x.Name == teamName);
            return new EmployeeTeamsNomenclatureTable(id, employeeId, teamTabel.Id);
        }

        private static List<EmployeesTable> Employees()
        {
            List<EmployeesTable> emploeyeesTable = new();
            List<RolesNomenclatureTable> roles = RolesNomenclature();

            string employeesJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataForSeed/employees.json"));
            List<EmployeesVM> data = JsonSerializer.Deserialize<List<EmployeesVM>>(employeesJson);

            foreach (var item in data.OrderBy(x => x.Id))
            {
                emploeyeesTable.Add(PrepareEmployeeTable(item, roles));
            }

            return emploeyeesTable;
        }

        private static EmployeesTable PrepareEmployeeTable(EmployeesVM vm, List<RolesNomenclatureTable> roles)
        {
            int id = vm.Id + 1;
            int roleNomenclatureId = 0;
            var role = roles.FirstOrDefault(x => x.Name == vm.Role);
            if (role != null)
            {
                roleNomenclatureId = role.Id;
            }

            return new EmployeesTable(id, vm.Name, vm.SurName, vm.Age, vm.Email, vm.ManagerId, roleNomenclatureId);
        }

        private static List<RolesNomenclatureTable> RolesNomenclature()
        {
            return new List<RolesNomenclatureTable>
            {
                 new RolesNomenclatureTable(1,"Manager"),
                 new RolesNomenclatureTable(2,"Junior Developer"),
                 new RolesNomenclatureTable(3,"Semi Senior Developer"),
                 new RolesNomenclatureTable(4,"Senior Developer"),
                 new RolesNomenclatureTable(5,"Principal"),
                 new RolesNomenclatureTable(6,"Team Leader")
            };
        }

        private static List<TeamsNomenclatureTable> TeamsNomenclature()
        {
            return new List<TeamsNomenclatureTable>
            {
                 new TeamsNomenclatureTable(1,"Platform"),
                 new TeamsNomenclatureTable(2,"Sales"),
                 new TeamsNomenclatureTable(3,"Billing"),
                 new TeamsNomenclatureTable(4,"Mirage")
            };
        }
    }
}
