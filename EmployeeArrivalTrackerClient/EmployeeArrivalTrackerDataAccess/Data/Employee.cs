﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeArrivalTrackerDataAccess.Data
{
    public class Employee
    {
        public Employee(int id,
                              string name,
                              string surName,
                              int age,
                              string email,
                              int? managerId,
                              int rolesNomenclatureId)
        {
            Id = id;
            Name = name;
            SurName = surName;
            Age = age;
            Email = email;
            ManagerId = managerId;
            RolesNomenclatureId = rolesNomenclatureId;
            EmployeeTeamsNomenclatures = new List<EmployeeTeamsNomenclature>();
        }

        [Key]
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string SurName { get; private set; }
        public int Age { get; private set; }

        public string Email { get; private set; }

        public int? ManagerId { get; private set; }

        public ICollection<EmployeeTeamsNomenclature>  EmployeeTeamsNomenclatures { get; private set; }

        public int RolesNomenclatureId { get; private set; }

        public RolesNomenclature RolesNomenclature { get; private set; }

        public EmployeeArrival EmployeeArrival { get; private set; }
    }
}
