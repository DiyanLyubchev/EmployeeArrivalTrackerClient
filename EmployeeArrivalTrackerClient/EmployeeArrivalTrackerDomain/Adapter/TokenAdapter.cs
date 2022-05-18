using Common.Models.Token;
using EmployeeArrivalTrackerDataAccess.Data;

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
