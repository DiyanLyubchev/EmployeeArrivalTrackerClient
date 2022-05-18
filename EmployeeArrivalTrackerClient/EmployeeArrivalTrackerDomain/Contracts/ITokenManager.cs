namespace EmployeeArrivalTrackerDomain.Contracts
{
    public interface ITokenManager
    {
        void AddTokenData(string tokenData);

        bool GetTokenIfExist(string token);
    }
}
