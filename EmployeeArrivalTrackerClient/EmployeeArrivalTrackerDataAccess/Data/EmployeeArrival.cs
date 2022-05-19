using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeArrivalTrackerDataAccess.Data
{
    public class EmployeeArrival
    {
        public EmployeeArrival(int employeeId, DateTime when)
        {
            this.EmployeeId = employeeId;
            this.When = when;
        }

        [Key]
        public int Id { get; set; }

        public int EmployeeId { get; private set; }

        public virtual Employee Employee { get; private set; }

        public DateTime When { get; private set; }
    }
}
