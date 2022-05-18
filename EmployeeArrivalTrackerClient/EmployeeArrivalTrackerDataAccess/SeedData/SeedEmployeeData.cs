using EmployeeArrivalTrackerDataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

        //public static void SeedEmployees(this ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<EmployeesTable>().HasData(Employees());
        //}

        //private static EmployeesTable[] Employees()
        //{
        //    List<RolesNomenclatureTable> roles = RolesNomenclature();
        //    List<TeamsNomenclatureTable> team = TeamsNomenclature();

        //    var employeesJson = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataForSeed/employees.json"));
        //  //  var data =  JsonSerializer.Deserialize<>

        //    return null;
        //}

        private static List<RolesNomenclatureTable> RolesNomenclature()
        {
            return new List<RolesNomenclatureTable>
            {
                 new RolesNomenclatureTable(1,"Junior Developer"),
                 new RolesNomenclatureTable(2,"Semi Senior Developer"),
                 new RolesNomenclatureTable(3,"Senior Developer"),
                 new RolesNomenclatureTable(4,"Principal"),
                 new RolesNomenclatureTable(5,"Team Leader")
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
