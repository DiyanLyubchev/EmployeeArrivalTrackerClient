namespace EmployeeArrivalTrackerDomain.Contracts
{
    public interface ITokenManager
    {
        bool AddTokenData(string tokenData);

        bool GetTokenIfExist(string token);
    }
}
