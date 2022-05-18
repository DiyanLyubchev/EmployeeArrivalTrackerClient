using EmployeeArrivalTrackerDataAccess.Data;

namespace EmployeeArrivalTrackerDataAccess.Contracts
{
    public interface ITokenDbManager
    {
        void AddToken(TokenTable token);

        TokenTable GetToken(string token);
    }
}
