using System;
using System.Text.Json.Serialization;

namespace Common.Models.Producer
{
    public record ProducerArrivalEmployeesVM
    (
    [property: JsonPropertyName("EmployeeId")] int EmployeeId,
    [property: JsonPropertyName("When")] DateTime When
    );
}
