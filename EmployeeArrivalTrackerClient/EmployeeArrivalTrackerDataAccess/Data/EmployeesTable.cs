using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeArrivalTrackerDataAccess.Data
{
    public class EmployeesTable
    {
        public EmployeesTable(int id,
                              string name,
                              string surName,
                              int age,
                              string email,
                              int? managerId,
                              int rolesNomenclatureTableId)
        {
            Id = id;
            Name = name;
            SurName = surName;
            Age = age;
            Email = email;
            ManagerId = managerId;
            Teams = new();
            RolesNomenclatureTableId = rolesNomenclatureTableId;
        }

        [Key]
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string SurName { get; private set; }
        public int Age { get; private set; }

        public string Email { get; private set; }

        public int? ManagerId { get; private set; }

        public List<TeamsNomenclatureTable> Teams { get; private set; }

        public int RolesNomenclatureTableId { get; private set; }

        public RolesNomenclatureTable Role { get; private set; }
    }
}
