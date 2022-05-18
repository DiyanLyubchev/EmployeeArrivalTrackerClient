using System.ComponentModel.DataAnnotations;

namespace EmployeeArrivalTrackerDataAccess.Data
{
    public class EmployeeTeamsNomenclatureTable
    {
        public EmployeeTeamsNomenclatureTable(int id, int employeesTableId,
            int teamsNomenclatureTableId)
        {
            Id = id;
            EmployeesTableId = employeesTableId;
            TeamsNomenclatureTableId = teamsNomenclatureTableId;
        }

        [Key]
        public int Id { get; private set; }

        public int EmployeesTableId { get; private set; }

        public EmployeesTable EmployeesTable { get; private set; }

        public int TeamsNomenclatureTableId { get; private set; }

        public TeamsNomenclatureTable TeamsNomenclatureTable { get; private set; }
    }
}
