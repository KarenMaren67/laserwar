
using Microsoft.Extensions.Configuration;

namespace DAL.Migrator
{
    public class PostgreSqlConnectionStringProvider : IConnectionStringProvider<string>
	{
		public PostgreSqlConnectionStringProvider(IConfiguration configuration)
		{
			ConnectionString = configuration.GetConnectionString("PostgreDbConnectionString");
		}

		public string ConnectionString { get; }
	}
}
