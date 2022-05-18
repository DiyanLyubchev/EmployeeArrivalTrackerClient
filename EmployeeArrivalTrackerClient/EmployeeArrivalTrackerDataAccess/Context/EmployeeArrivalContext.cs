using EmployeeArrivalTrackerDataAccess.Data;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
