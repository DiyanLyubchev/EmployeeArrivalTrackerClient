using AutoMapper;
using EmployeeArrivalTrackerDataAccess.Contracts;
using EmployeeArrivalTrackerDataAccess.Data;
using EmployeeArrivalTrackerDomain.Adapter;
using EmployeeArrivalTrackerDomain.Contracts;
using EmployeeArrivalTrackerDomain.Models;
using EmployeeArrivalTrackerDomain.Models.Producer;
using System.Collections.Generic;
using System.Text.Json;

namespace EmployeeArrivalTrackerDomain.Application
{
    public class EmployeeArrivalManager : IEmployeeArrivalManager
    {
        private readonly IMapper mapper;
        private readonly IEmployeeArrivalDbManager dbManager;

        public EmployeeArrivalManager(IMapper mapper, IEmployeeArrivalDbManager dbManager)
        {
            this.mapper = mapper;
            this.dbManager = dbManager;
        }

        public List<ArrivalEmployeeVM> GetAllArrivalEmployees()
        {
            var emplData = this.dbManager.GetAllArrivalEmployees();

            if (emplData.Count > 0)
            {
                return this.mapper.Map<List<ArrivalEmployeeVM>>(emplData);
            }

            return new List<ArrivalEmployeeVM>();
        }

        public bool AddArrivalAmployees(object data, string token)
        {
            if (data != null)
            {
                string dataAsString = data.ToString();
                var request = JsonSerializer.Deserialize<List<Employee>>(dataAsString);
                List<EmployeeArrivalTable> tables = EmployeeAdapter.Transform(request);
               
                this.dbManager.AddArrivalEmployees(tables);
                return true;
            }

            return false;
        }
    }
}
