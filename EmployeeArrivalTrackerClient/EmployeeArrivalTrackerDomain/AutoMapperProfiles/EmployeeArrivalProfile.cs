using AutoMapper;
using EmployeeArrivalTrackerDataAccess.Data;
using EmployeeArrivalTrackerDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeArrivalTrackerDomain.AutoMapperProfiles
{
    public class EmployeeArrivalProfile : Profile
    {
        public EmployeeArrivalProfile()
        {
            CreateMap<ArrivalEmployeeVM, EmployeeArrivalTable>().ReverseMap();
        }
    }
}
