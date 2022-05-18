using System.ComponentModel.DataAnnotations;

namespace EmployeeArrivalTrackerDataAccess.Data
{
    public class TeamsNomenclatureTable
    {
        public TeamsNomenclatureTable(int id, string name)
        {
            Id = id;
            Name = name;
        }

        [Key]
        public int Id { get; private set; }

        public string Name { get; set; }
    }
}
