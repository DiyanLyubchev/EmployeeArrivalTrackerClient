using Common;
using Common.Models.Employees;
using Common.Models.Producer;
using EmployeeArrivalTrackerDataAccess.Contracts;
using EmployeeArrivalTrackerDataAccess.Data;
using EmployeeArrivalTrackerDomain.Adapter;
using EmployeeArrivalTrackerDomain.Contracts;
using System;
using System.Collections.Generic;

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

        public List<EmployeesVM> GetAllArrivalEmployees()
        {
            //var emplData = this.dbManager.GetAllArrivalEmployeesByQuery(); //too slowly
            //return EmployeeAdapter.Transform(emplData);

            var emplData = this.dbManager.GetAllArrivalEmployeesByEF();
            return emplData;
        }

        public bool AddArrivalEmployees(List<ProducerArrivalEmployeesVM> data, string token)
        {
            bool isTokenValid = this.tokenManager.GetTokenIfExist(token);

            if (data.Count != 0 && isTokenValid)
            {
                List<EmployeeArrival> tables = EmployeeAdapter.Transform(data);
                this.dbManager.AddArrivalEmployees(tables);

                return true;
            }

            return false;
        }
    }
}
