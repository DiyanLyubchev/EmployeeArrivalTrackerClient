using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeArrivalTrackerDataAccess.Data
{
    public class TeamsNomenclatureTable
    {
        public TeamsNomenclatureTable(int id, string name)
        {
            Id = id;
            Name = name;
            EmployeesTables = new();
        }

        [Key]
        public int Id { get; private set; }

        public string Name { get; set; }

        public List<EmployeesTable> EmployeesTables { get; private set; }
    }
}
