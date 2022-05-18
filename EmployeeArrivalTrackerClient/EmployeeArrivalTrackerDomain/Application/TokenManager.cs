using Common;
using EmployeeArrivalTrackerDataAccess.Contracts;
using EmployeeArrivalTrackerDataAccess.Data;
using EmployeeArrivalTrackerDomain.Adapter;
using EmployeeArrivalTrackerDomain.Contracts;
using EmployeeArrivalTrackerDomain.Models.Token;
using System;
using System.Text.Json;

namespace EmployeeArrivalTrackerDomain.Application
{
    public class TokenManager : ITokenManager
    {
        private readonly ITokenDbManager dbManager;

        public TokenManager(ITokenDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public void AddTokenData(string tokenData)
        {
            if (!string.IsNullOrEmpty(tokenData))
            {
                TokenModel tokenModel = JsonSerializer.Deserialize<TokenModel>(tokenData);
                TokenTable table = TokenAdapter.Transform(tokenModel);
                this.dbManager.AddToken(table);
            }
        }

        public bool GetTokenIfExist(string token)
        {
            var tableToken = this.dbManager.GetToken(token);

            if (tableToken != null)
            {
                DateTime currentDate = Utils.GetCurrentDate();

                bool isNotExpires = currentDate < tableToken.Expires;

                return isNotExpires;
            }

            return false;
        }
    }
}
