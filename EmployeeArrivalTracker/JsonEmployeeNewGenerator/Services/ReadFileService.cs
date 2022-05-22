using JsonEmployeeNewGenerator.Common;
using JsonEmployeeNewGenerator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JsonEmployeeNewGenerator.Services
{
    public static class  ReadFileService
    {
        public static List<JsonEmployee> ReadFile()
        {
            var generator = new Random();
            var allLinesInFile = File.ReadAllLines("employees.txt").ToArray();

            var employees = new List<JsonEmployee>();
            for (int i = 0; i < allLinesInFile.Length; i++)
            {
                JsonEmployee e = new JsonEmployee();
                e.Id = i;
                e.Name = allLinesInFile[i].Split('\t')[0];
                e.SurName = allLinesInFile[i].Split('\t')[1];
                e.Email = allLinesInFile[i].Split('\t')[2];
                e.Age = generator.Next(18, 66);
                if (i < 11)
                {
                    e.Role = "Manager";
                    e.Teams = new HashSet<string>();
                }
                else
                {
                    e.ManagerId = generator.Next(11);
                    e.Role = Nomenclature.roles[generator.Next(4)];
                    int count = generator.Next(1, 4);
                    var employeeTeams = new HashSet<string>();
                    for (int j = 0; j < count; ++j)
                    {
                        employeeTeams.Add(Nomenclature.teams[generator.Next(4)]);
                    }
                    e.Teams = employeeTeams;
                }

                employees.Add(e);
            }

            return employees;
        }
    }
}
