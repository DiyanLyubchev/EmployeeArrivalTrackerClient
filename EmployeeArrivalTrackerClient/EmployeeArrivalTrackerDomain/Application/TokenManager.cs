using Common;
using Common.Exceptions;
using Common.Models.Token;
using EmployeeArrivalTrackerDataAccess.Contracts;
using EmployeeArrivalTrackerDataAccess.Data;
using EmployeeArrivalTrackerDomain.Adapter;
using EmployeeArrivalTrackerDomain.Contracts;
using System;
using System.Runtime.CompilerServices;
using System.Text.Json;

[assembly: InternalsVisibleTo("EmployeeArrivalTrackerInfrastructure")]
[assembly: InternalsVisibleTo("EmployeeArrivalTrackerClient")]
[assembly: InternalsVisibleTo("EmployeeArrivalTrackerTest")]
namespace EmployeeArrivalTrackerDomain.Application
{ 
    internal class TokenManager : ITokenManager
    {
        private IGenericRepository<Tokens> repository;
        public TokenManager(IGenericRepository<Tokens> repository)
        {
            this.repository = repository;
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
                this.repository.Insert(table);
                this.repository.Save();

                return true;
            }

            return false;
        }

        public bool GetTokenIfExist(string token)
        {
            var tableToken = this.repository.GetFirstOrDefault(x => x.Token == token);

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
