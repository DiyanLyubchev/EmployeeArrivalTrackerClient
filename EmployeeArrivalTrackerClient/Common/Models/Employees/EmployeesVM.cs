using System;
using System.Collections.Generic;

namespace Common.Models.Employees
{
    public class EmployeesVM
    {
        public int Id { get; set; }

        public int? ManagerId { get; set; }

        public int Age { get; set; }

        public HashSet<string> Teams { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public string SurName { get; set; }

        public string Name { get; set; }

        public DateTime When { get; set; }
    }
}
