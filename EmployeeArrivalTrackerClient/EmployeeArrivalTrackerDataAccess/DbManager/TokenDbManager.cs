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

        public void AddToken(Tokens token)
        {
            this.context.Tokens.Add(token);
            this.context.SaveChanges();
        }

        public Tokens GetToken(string token)
        {
           return this.context.Tokens.FirstOrDefault(x => x.Token == token);
        }
    }
}
