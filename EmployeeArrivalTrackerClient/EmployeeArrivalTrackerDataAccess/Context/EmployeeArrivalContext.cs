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

        public virtual DbSet<EmployeeArrival> EmployeeArrivals { get; set; }

        public virtual DbSet<Tokens> Tokens { get; set; }

        public virtual DbSet<TeamsNomenclature>  Teams { get; set; }

        public virtual DbSet<RolesNomenclature>  Roles { get; set; }

        public virtual DbSet<EmployeeTeamsNomenclature> EmployeeTeamsNomenclatures { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

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
