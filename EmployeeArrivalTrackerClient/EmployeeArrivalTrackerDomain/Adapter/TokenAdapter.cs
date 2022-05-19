using Common.Models.Token;
using EmployeeArrivalTrackerDataAccess.Data;

namespace EmployeeArrivalTrackerDomain.Adapter
{
    public static class TokenAdapter
    {
        public static Tokens Transform(TokenModel token)
        {
            return new Tokens(token.Token, token.Expires);
        }
    }
}
