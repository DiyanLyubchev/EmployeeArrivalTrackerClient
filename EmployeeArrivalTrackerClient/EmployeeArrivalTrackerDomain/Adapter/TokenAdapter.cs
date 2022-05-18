using EmployeeArrivalTrackerDataAccess.Data;
using EmployeeArrivalTrackerDomain.Models.Token;

namespace EmployeeArrivalTrackerDomain.Adapter
{
    public static class TokenAdapter
    {
        public static TokenTable Transform(TokenModel token)
        {
            return new TokenTable(token.Token, token.Expires);
        }
    }
}
