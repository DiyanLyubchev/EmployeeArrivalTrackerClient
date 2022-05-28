using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeArrivalTrackerDataAccess.Data
{
    public class Employee
    {
        public Employee(int id,
                              string name,
                              string surName,
                              int? age,
                              string email,
                              int? managerId,
                              int? rolesNomenclatureId)
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
        public int? Age { get; private set; }

        public string Email { get; private set; }

        public int? ManagerId { get; private set; }

        public virtual ICollection<EmployeeTeamsNomenclature>  EmployeeTeamsNomenclatures { get; private set; }

        [ForeignKey(nameof(RolesNomenclature))]
        public int? RolesNomenclatureId { get; private set; }

        public virtual RolesNomenclature RolesNomenclature { get; private set; }

        public virtual EmployeeArrival EmployeeArrival { get; private set; }
    }
}
