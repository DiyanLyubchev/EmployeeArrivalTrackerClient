using System;
using System.Text.Json.Serialization;

namespace Common.Models.Token
{
    public record TokenModel
        (
        [property: JsonPropertyName("token")] string Token, 
        [property: JsonPropertyName("expires")] DateTime Expires
        );
}
