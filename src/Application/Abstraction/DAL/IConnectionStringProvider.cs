namespace Application.Abstraction.DAL
{
	public interface IConnectionStringProvider<out T>
	{
		/// <summary>
		/// Строка подключения
		/// </summary>
		T ConnectionString { get; }
	}
}
