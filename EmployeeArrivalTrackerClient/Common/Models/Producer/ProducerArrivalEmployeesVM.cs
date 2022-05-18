using System;
using System.Text.Json.Serialization;

namespace Common.Models.Producer
{
    public class ProducerArrivalEmployeesVM
    {
        [JsonPropertyName("EmployeeId")]
        public int EmployeeId { get; set; }

        [JsonPropertyName("When")]
        public DateTime When { get; set; }
    }
}
