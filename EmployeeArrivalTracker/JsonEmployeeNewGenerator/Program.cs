using JsonEmployeeNewGenerator.Models;
using JsonEmployeeNewGenerator.Services;
using System.Collections.Generic;

namespace JsonEmployeeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<JsonEmployee> employees = ReadFileService.ReadFile();

            WriteFileService.WriteFile(employees);
        }
    }
}
