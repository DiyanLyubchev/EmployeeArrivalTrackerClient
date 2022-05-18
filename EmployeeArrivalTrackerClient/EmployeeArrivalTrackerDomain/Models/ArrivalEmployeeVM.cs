using System;

namespace EmployeeArrivalTrackerDomain.Models
{
    public class ArrivalEmployeeVM
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime When { get; set; }
    }
}
