using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeArrivalTrackerDataAccess.Data
{
    public class EmployeeTeamsNomenclature
    {
        public EmployeeTeamsNomenclature(int id, int employeeId,
            int teamsNomenclatureId)
        {
            Id = id;
            EmployeeId = employeeId;
            TeamsNomenclatureId = teamsNomenclatureId;
        }

        [Key]
        public int Id { get; private set; }

        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; private set; }

        public virtual Employee Employee { get; private set; }

        [ForeignKey(nameof(TeamsNomenclature))]
        public int TeamsNomenclatureId { get; private set; }

        public virtual TeamsNomenclature TeamsNomenclature { get; private set; }
    }
}
