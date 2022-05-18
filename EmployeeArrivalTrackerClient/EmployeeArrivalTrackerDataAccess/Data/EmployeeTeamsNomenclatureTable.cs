using System.ComponentModel.DataAnnotations;

namespace EmployeeArrivalTrackerDataAccess.Data
{
    public class EmployeeTeamsNomenclatureTable
    {
        [Key]
        public int Id { get; private set; }

        public int EmployeesTableId { get; private set; }

        public EmployeesTable EmployeesTable { get; private set; }

        public int TeamsNomenclatureTableId { get; private set; }

        public TeamsNomenclatureTable TeamsNomenclatureTable { get; private set; }
    }
}
