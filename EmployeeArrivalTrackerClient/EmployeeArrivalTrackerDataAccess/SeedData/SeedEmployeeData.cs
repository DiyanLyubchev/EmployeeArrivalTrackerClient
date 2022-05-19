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
        private static string employeesJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataForSeed/employees.json"));

        public static void SeedTeamsNomenclature(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamsNomenclature>().HasData(TeamsNomenclature());
        }

        public static void SeedRolesNomenclature(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolesNomenclature>().HasData(RolesNomenclature());
        }

        public static void SeedEmployees(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(Employees());
        }

        public static void SeedEmployeeTeamsNomenclature(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeTeamsNomenclature>().HasData(EmployeeTeamsNomenclature());
        }

        private static List<EmployeeTeamsNomenclature> EmployeeTeamsNomenclature()
        {
            List<EmployeeTeamsNomenclature> emploeyeeTeamsTable = new();
            List<TeamsNomenclature> teams = TeamsNomenclature();

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

        private static EmployeeTeamsNomenclature PrepareEmployeesTeamsTable(string teamName, List<TeamsNomenclature> teams, int employeeId, int id)
        {
            var teamTabel = teams.First(x => x.Name == teamName);
            return new EmployeeTeamsNomenclature(id, employeeId, teamTabel.Id);
        }

        private static List<Employee> Employees()
        {
            List<Employee> emploeyeesTable = new();
            List<RolesNomenclature> roles = RolesNomenclature();

            List<EmployeesVM> data = JsonSerializer.Deserialize<List<EmployeesVM>>(employeesJson);

            foreach (var item in data.OrderBy(x => x.Id))
            {
                emploeyeesTable.Add(PrepareEmployeeTable(item, roles));
            }

            return emploeyeesTable;
        }

        private static Employee PrepareEmployeeTable(EmployeesVM vm, List<RolesNomenclature> roles)
        {
            int id = vm.Id + 1;
            int roleNomenclatureId = 0;
            var role = roles.FirstOrDefault(x => x.Name == vm.Role);
            if (role != null)
            {
                roleNomenclatureId = role.Id;
            }

            return new Employee(id, vm.Name, vm.SurName, vm.Age, vm.Email, vm.ManagerId, roleNomenclatureId);
        }

        private static List<RolesNomenclature> RolesNomenclature()
        {
            return new List<RolesNomenclature>
            {
                 new RolesNomenclature(1,"Manager"),
                 new RolesNomenclature(2,"Junior Developer"),
                 new RolesNomenclature(3,"Semi Senior Developer"),
                 new RolesNomenclature(4,"Senior Developer"),
                 new RolesNomenclature(5,"Principal"),
                 new RolesNomenclature(6,"Team Leader")
            };
        }

        private static List<TeamsNomenclature> TeamsNomenclature()
        {
            return new List<TeamsNomenclature>
            {
                 new TeamsNomenclature(1,"Platform"),
                 new TeamsNomenclature(2,"Sales"),
                 new TeamsNomenclature(3,"Billing"),
                 new TeamsNomenclature(4,"Mirage")
            };
        }
    }
}
