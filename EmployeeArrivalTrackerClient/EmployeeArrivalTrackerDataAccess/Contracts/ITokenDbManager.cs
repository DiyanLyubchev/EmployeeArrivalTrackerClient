using EmployeeArrivalTrackerDataAccess.Data;

namespace EmployeeArrivalTrackerDataAccess.Contracts
{
    public interface ITokenDbManager
    {
        void AddToken(Tokens token);

        Tokens GetToken(string token);
    }
}
