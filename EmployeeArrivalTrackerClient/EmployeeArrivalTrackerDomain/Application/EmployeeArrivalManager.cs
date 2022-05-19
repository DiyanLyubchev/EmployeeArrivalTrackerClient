using Common;
using Common.Models.Employees;
using Common.Models.Producer;
using Common.Pagination;
using EmployeeArrivalTrackerDataAccess.Contracts;
using EmployeeArrivalTrackerDataAccess.Data;
using EmployeeArrivalTrackerDomain.Adapter;
using EmployeeArrivalTrackerDomain.Contracts;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace EmployeeArrivalTrackerDomain.Application
{
    public class EmployeeArrivalManager : IEmployeeArrivalManager
    {
        private readonly IEmployeeArrivalDbManager dbManager;
        private readonly ITokenManager tokenManager;
        public EmployeeArrivalManager(IEmployeeArrivalDbManager dbManager, ITokenManager tokenManager)
        {
            this.dbManager = dbManager;
            this.tokenManager = tokenManager;
        }

        public PagedResult<EmployeesVM> GetAllArrivalEmployees(int p)
        {
            DateTime currentDate = Utils.GetCurrentDate();
            var emplData = this.dbManager.GetAllArrivalEmployeesBySpecificDate(currentDate, p);

            return emplData;
        }

        public void AddArrivalAmployees(object data, string token)
        {
            if (data != null)
            {
                string dataAsString = data.ToString();
                List<ProducerArrivalEmployeesVM> request = JsonSerializer.Deserialize<List<ProducerArrivalEmployeesVM>>(dataAsString);

                bool isTokenValid = this.tokenManager.GetTokenIfExist(token);

                this.AddArrivalEmployeeHelper(request, isTokenValid);
            }
        }

        private void AddArrivalEmployeeHelper(List<ProducerArrivalEmployeesVM> request, bool isTokenValid)
        {
            if (isTokenValid)
            {
                List<EmployeeArrival> tables = EmployeeAdapter.Transform(request);
                this.dbManager.AddArrivalEmployees(tables);
            }
        }
    }
}
