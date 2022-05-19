using AutoMapper;
using EmployeeArrivalTrackerDataAccess.Data;
using EmployeeArrivalTrackerDomain.Models;

namespace EmployeeArrivalTrackerClient.AutoMapperProfiles
{
    public class EmployeeArrivalProfile : Profile
    {
        public EmployeeArrivalProfile()
        {
            CreateMap<ArrivalEmployeeVM, EmployeeArrival>().ReverseMap();
        }
    }
}
