using System.Linq;

namespace Application.Abstraction.DAL
{
	public interface IRepository<TBusinessEntity, TStorageEntity>
		where TBusinessEntity : class
		where TStorageEntity : class
	{
		IQueryable<TBusinessEntity> GetAll();

		TBusinessEntity Get(int id);

		TBusinessEntity Insert(TBusinessEntity entity);

		void Delete(int id);

		TBusinessEntity Update(TBusinessEntity entity);
	}
}
