using JsonEmployeeNewGenerator.Common;
using JsonEmployeeNewGenerator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JsonEmployeeNewGenerator.Services
{
    public static class ReadFileService
    {
        public static List<JsonEmployee> ReadFile()
        {
            Random generator = new();
            var allLinesInFile = File.ReadAllLines("employees.txt").ToArray();

            List<JsonEmployee> employees = new();
            for (int i = 0; i < allLinesInFile.Length; i++)
            {
                JsonEmployee employee = new();
                employee.Id = i;
                employee.Name = allLinesInFile[i].Split('\t')[0];
                employee.SurName = allLinesInFile[i].Split('\t')[1];
                employee.Email = allLinesInFile[i].Split('\t')[2];
                employee.Age = generator.Next(18, 66);
                if (i < 11)
                {
                    employee.Role = "Manager";
                    employee.Teams = new HashSet<string>();
                }
                else
                {
                    employee.ManagerId = generator.Next(11);
                    employee.Role = Nomenclature.roles[generator.Next(4)];
                    int count = generator.Next(1, 4);
                    HashSet<string> employeeTeams = new();
                    for (int j = 0; j < count; ++j)
                    {
                        employeeTeams.Add(Nomenclature.teams[generator.Next(4)]);
                    }
                    employee.Teams = employeeTeams;
                }

                employees.Add(employee);
            }

            return employees;
        }
    }
}
