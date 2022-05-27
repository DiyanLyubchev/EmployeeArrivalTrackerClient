using JsonEmployeeNewGenerator.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JsonEmployeeNewGenerator.Services
{
    public static class WriteFileService
    {
        private static readonly JsonSerializerOptions _options =
                        new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };

        public static void WriteFile(List<JsonEmployee> employees)
        {
            JsonSerializerOptions options = new(_options)
            {
                WriteIndented = true
            };

            string jsonEmployees = JsonSerializer.Serialize(employees, options);
            File.WriteAllText(@$"newEmployees.json", jsonEmployees);
        }
    }
}
