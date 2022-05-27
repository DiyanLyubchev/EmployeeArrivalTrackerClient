using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeArrivalTrackerDataAccess.Data
{
    public class EmployeeArrival
    {
        public EmployeeArrival(int employeeId, DateTime whenArrival)
        {
            this.EmployeeId = employeeId;
            this.WhenArrival = whenArrival;
        }

        [Key]
        public int Id { get; set; }

        public int EmployeeId { get; private set; }

        public virtual Employee Employee { get; private set; }

        public DateTime WhenArrival { get; private set; }
    }
}
