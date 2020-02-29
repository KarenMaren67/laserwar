using Application.Abstraction.DAL;
using Microsoft.Extensions.Configuration;

namespace DAL.EF
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