using Common.Models.Token;
using EmployeeArrivalTrackerDataAccess.Data;
using System;
using System.Text.Json;

namespace EmployeeArrivalTrackerTest.Util
{
    public static class TokensGenerator
    {
        public static string GenerateTokenModelWithNullToken()
        {
            var tokenModel = new TokenModel(null, DateTime.Now.AddHours(5));
            return JsonSerializer.Serialize(tokenModel);
        }

        public static string GenerateTokenModel()
        {
            var tokenModel = new TokenModel(Guid.NewGuid().ToString(), DateTime.Now.AddHours(5));
            return JsonSerializer.Serialize(tokenModel);
        }

        public static Tokens GenerateTokenTable()
        {
            return new Tokens("f16149b14cba46be92da20743b97a2f2", DateTime.Now.AddHours(5));
        }
    }
}
