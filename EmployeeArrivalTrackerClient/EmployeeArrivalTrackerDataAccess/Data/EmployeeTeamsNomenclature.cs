using System.ComponentModel.DataAnnotations;

namespace EmployeeArrivalTrackerDataAccess.Data
{
    public class EmployeeTeamsNomenclature
    {
        public EmployeeTeamsNomenclature(int id, int employeesTableId,
            int teamsNomenclatureTableId)
        {
            Id = id;
            EmployeesTableId = employeesTableId;
            TeamsNomenclatureTableId = teamsNomenclatureTableId;
        }

        [Key]
        public int Id { get; private set; }

        public int EmployeesTableId { get; private set; }

        public Employee EmployeesTable { get; private set; }

        public int TeamsNomenclatureTableId { get; private set; }

        public TeamsNomenclature TeamsNomenclatureTable { get; private set; }
    }
}
