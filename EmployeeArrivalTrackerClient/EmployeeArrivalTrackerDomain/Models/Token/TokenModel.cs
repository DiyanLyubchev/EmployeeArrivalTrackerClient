using System;
using System.Text.Json.Serialization;

namespace EmployeeArrivalTrackerDomain.Models.Token
{
    public class TokenModel
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("expires")]
        public DateTime Expires { get; set; }
    }
}
