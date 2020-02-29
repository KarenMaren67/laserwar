using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Migrator.Context;
using System.IO;

namespace Migrator
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EFDataContext>
    {
        public IConfiguration Configuration { get; set; }
        public EFDataContext CreateDbContext(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

           return new EFDataContext(new PostgreSqlConnectionStringProvider(Configuration)); 
        }
    }
}
