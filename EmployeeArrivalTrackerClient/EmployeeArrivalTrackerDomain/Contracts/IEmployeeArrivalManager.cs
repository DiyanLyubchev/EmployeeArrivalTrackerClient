using Common.Models.Employees;
using Common.Pagination;

namespace EmployeeArrivalTrackerDomain.Contracts
{
    public interface IEmployeeArrivalManager
    {
        PagedResult<EmployeesVM> GetAllArrivalEmployees(int p);

        void AddArrivalAmployees(object data, string token);
    }
}
