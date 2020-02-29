using DataAccessLayer.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DAL.EF
{
    public class EFDataContextFactory : IDesignTimeDbContextFactory<EFDataContext>
    {
        public EFDataContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json");

            var configuration = configurationBuilder.Build();

            var connectionStringProvider = new PostgreSqlConnectionStringProvider(configuration);

            return new EFDataContext(connectionStringProvider);
        }
    }
}
