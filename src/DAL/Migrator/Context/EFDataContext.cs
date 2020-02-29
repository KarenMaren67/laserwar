namespace Migrator.Context
{
    public class EFDataContext : DbContext
    {
        private readonly string _connectionString;

        public EFDataContext(PostgreSqlConnectionStringProvider connectionString)
          : base()
        {
            _connectionString = connectionString.ConnectionString;
        }

        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }
}
