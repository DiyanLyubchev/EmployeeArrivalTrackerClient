using AutoMapper;
using Common;
using EmployeeArrivalTrackerDataAccess.Contracts;
using EmployeeArrivalTrackerDataAccess.Data;
using EmployeeArrivalTrackerDomain.Adapter;
using EmployeeArrivalTrackerDomain.Contracts;
using EmployeeArrivalTrackerDomain.Models;
using EmployeeArrivalTrackerDomain.Models.Producer;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace EmployeeArrivalTrackerDomain.Application
{
    public class EmployeeArrivalManager : IEmployeeArrivalManager
    {
        private readonly IMapper mapper;
        private readonly IEmployeeArrivalDbManager dbManager;
        private readonly ITokenManager tokenManager;
        public EmployeeArrivalManager(IMapper mapper, IEmployeeArrivalDbManager dbManager, ITokenManager tokenManager)
        {
            this.mapper = mapper;
            this.dbManager = dbManager;
            this.tokenManager = tokenManager;
        }

        public List<ArrivalEmployeeVM> GetAllArrivalEmployees()
        {
            DateTime currentDate = Utils.GetCurrentDate();

            var emplData = this.dbManager.GetAllArrivalEmployeesBySpecificDate(currentDate);

            if (emplData.Count > 0)
            {
                return this.mapper.Map<List<ArrivalEmployeeVM>>(emplData);
            }

            return new List<ArrivalEmployeeVM>();
        }

        public void AddArrivalAmployees(object data, string token)
        {
            if (data != null)
            {
                string dataAsString = data.ToString();
                List<Employee> request = JsonSerializer.Deserialize<List<Employee>>(dataAsString);

                bool isTokenValid = this.tokenManager.GetTokenIfExist(token);

                this.AddArrivalEmployeeHelper(request, isTokenValid);
            }
        }

        private void AddArrivalEmployeeHelper(List<Employee> request, bool isTokenValid)
        {
            if (isTokenValid)
            {
                List<EmployeeArrivalTable> tables = EmployeeAdapter.Transform(request);
                this.dbManager.AddArrivalEmployees(tables);
            }
        }
    }
}
