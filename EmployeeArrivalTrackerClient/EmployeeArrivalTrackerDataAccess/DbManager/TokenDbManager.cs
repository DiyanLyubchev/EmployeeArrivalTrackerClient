using EmployeeArrivalTrackerDataAccess.Context;
using EmployeeArrivalTrackerDataAccess.Contracts;
using EmployeeArrivalTrackerDataAccess.Data;
using System.Linq;

namespace EmployeeArrivalTrackerDataAccess.DbManager
{
    public class TokenDbManager : ITokenDbManager
    {
        private EmployeeArrivalContext context;

        public TokenDbManager(EmployeeArrivalContext context)
        {
            this.context = context;
        }

        public void AddToken(TokenTable token)
        {
            this.context.TokenTables.Add(token);
            this.context.SaveChanges();
        }

        public TokenTable GetToken(string token)
        {
           return this.context.TokenTables.FirstOrDefault(x => x.Token == token);
        }
    }
}
