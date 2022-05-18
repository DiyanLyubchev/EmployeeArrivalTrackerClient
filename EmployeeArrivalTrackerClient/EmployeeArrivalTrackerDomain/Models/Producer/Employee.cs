using System;
using System.Text.Json.Serialization;

namespace EmployeeArrivalTrackerDomain.Models.Producer
{
    public class Employee
    {
        [JsonPropertyName("EmployeeId")]
        public int EmployeeId { get; set; }

        [JsonPropertyName("When")]
        public DateTime When { get; set; }
    }
}
