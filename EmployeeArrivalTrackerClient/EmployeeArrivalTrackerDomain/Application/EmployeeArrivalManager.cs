using Common.Models.Employees;
using Common.Models.Producer;
using EmployeeArrivalTrackerDataAccess.Contracts;
using EmployeeArrivalTrackerDataAccess.Data;
using EmployeeArrivalTrackerDomain.Adapter;
using EmployeeArrivalTrackerDomain.Contracts;
using System.Collections.Generic;

namespace EmployeeArrivalTrackerDomain.Application
{
    public class EmployeeArrivalManager : IEmployeeArrivalManager
    {
        private readonly IGenericRepository<EmployeeArrival> repositoryEmployeeArrival;
        private readonly IGenericRepository<EmployeeReport> repositoryEmployee;
        private readonly ITokenManager tokenManager;
        public EmployeeArrivalManager(IGenericRepository<EmployeeArrival> repositoryEmployeeArrival,
                                      ITokenManager tokenManager,
                                      IGenericRepository<EmployeeReport> repositoryEmployee)
        {
            this.repositoryEmployeeArrival = repositoryEmployeeArrival;
            this.repositoryEmployee = repositoryEmployee;
            this.tokenManager = tokenManager;
        }

        public List<EmployeesVM> GetAllArrivalEmployees()
        {
            string query = @$"select 
                              e.Id,
                              e.Name, 
                              e.SurName, 
                              e.Age, 
                              e.Email, 
                              e.ManagerId, 
                              t.Name TeamName,
                              r.Name RoleName,
                              a.WhenArrival 
                              from dbo.Employees e 
                              join dbo.EmployeeArrivals a on e.Id = a.EmployeeId
                              join dbo.Roles r on e.RolesNomenclatureId = r.Id
                              join dbo.EmployeeTeamsNomenclatures en on e.Id = en.EmployeeId
                              join dbo.Teams t on en.TeamsNomenclatureId = t.Id";

            var emplData = this.repositoryEmployee.GetQueryableByQuery(query);
                                                  
            return EmployeeAdapter.Transform(emplData);
        }

        public bool AddArrivalEmployees(List<ProducerArrivalEmployeesVM> data, string token)
        {
            bool isTokenValid = this.tokenManager.GetTokenIfExist(token);

            if (data.Count != 0 && isTokenValid)
            {
                List<EmployeeArrival> tables = EmployeeAdapter.Transform(data);
                this.repositoryEmployeeArrival.InsertAll(tables);
                this.repositoryEmployeeArrival.Save();

                return true;
            }

            return false;
        }
    }
}
