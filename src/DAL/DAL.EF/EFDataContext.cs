using Application.Abstraction.DAL;
using Domain.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DAL.EF
{
    public class EFDataContext : DbContext
    {
        private readonly string _connectionString;

        public EFDataContext(IConnectionStringProvider<string> connectionStringProvider)
          : base()
        {
            _connectionString = connectionStringProvider.ConnectionString;
        }

        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }
}
