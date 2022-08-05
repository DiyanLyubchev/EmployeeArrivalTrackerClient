using System;

namespace EmployeeArrivalTrackerDataAccess.Data
{
    public class EmployeeReport
    {
        public int Id { get; set; }

        public  string Name { get; set; }

        public  string SurName { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public int ManagerId { get; set; }

        public string TeamName { get; set; }

        public string RoleName { get; set; }

        public DateTime WhenArrival { get; private set; }
    }
}
