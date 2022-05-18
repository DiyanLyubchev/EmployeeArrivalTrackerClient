using System.Threading.Tasks;

namespace EmployeeArrivalTrackerInfrastructure.Contracts
{
    public interface IClientsService
    {
        Task<string> CallCliensServiceAsync();
    }
}