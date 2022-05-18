using EmployeeArrivalTrackerDataAccess.Data;
using EmployeeArrivalTrackerDataAccess.SeedData;
using Microsoft.EntityFrameworkCore;

namespace EmployeeArrivalTrackerDataAccess.Context
{
    public class EmployeeArrivalContext : DbContext
    {
        public EmployeeArrivalContext(DbContextOptions<EmployeeArrivalContext> options)
            : base(options)
        {

        }

        public virtual DbSet<EmployeeArrivalTable> EmployeeArrivalTable { get; set; }

        public virtual DbSet<TokenTable> TokenTables { get; set; }

        public virtual DbSet<TeamsNomenclatureTable> TeamsNomenclatureTables { get; set; }

        public virtual DbSet<RolesNomenclatureTable> RolesNomenclatureTables { get; set; }

        public virtual DbSet<EmployeeTeamsNomenclatureTable> EmployeeTeamsNomenclatureTables { get; set; }

        public virtual DbSet<EmployeesTable> EmployeesTables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedTeamsNomenclature();
            modelBuilder.SeedRolesNomenclature();
            modelBuilder.SeedEmployees();
            modelBuilder.SeedEmployeeTeamsNomenclature();
            base.OnModelCreating(modelBuilder);
        }
    }
}
