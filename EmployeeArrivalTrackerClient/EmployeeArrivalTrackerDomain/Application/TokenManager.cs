using EmployeeArrivalTrackerDataAccess.Contracts;
using EmployeeArrivalTrackerDataAccess.Data;
using EmployeeArrivalTrackerDomain.Adapter;
using EmployeeArrivalTrackerDomain.Contracts;
using EmployeeArrivalTrackerDomain.Models.Token;
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
            try
            {
                if (!string.IsNullOrEmpty(tokenData))
                {
                    TokenModel tokenModel = JsonSerializer.Deserialize<TokenModel>(tokenData);
                    TokenTable table = TokenAdapter.Transform(tokenModel);
                    this.dbManager.AddToken(table);
                }
            }
            catch (System.Exception)
            {
            }
        }
    }
}
