using Common;
using Common.Exceptions;
using Common.Models.Token;
using EmployeeArrivalTrackerDataAccess.Contracts;
using EmployeeArrivalTrackerDataAccess.Data;
using EmployeeArrivalTrackerDomain.Adapter;
using EmployeeArrivalTrackerDomain.Contracts;
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

        public bool AddTokenData(string tokenData)
        {
            if (!string.IsNullOrEmpty(tokenData))
            {
                TokenModel tokenModel = JsonSerializer.Deserialize<TokenModel>(tokenData);

                if (string.IsNullOrEmpty(tokenModel.Token))
                {
                    throw new TokenException("Token can not be null");
                }

                Tokens table = TokenAdapter.Transform(tokenModel);
                this.dbManager.AddToken(table);

                return true;
            }

            return false;
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
