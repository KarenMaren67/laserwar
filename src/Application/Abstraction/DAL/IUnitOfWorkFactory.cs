namespace Application.Abstraction.DAL
{
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// Создать UoW
        /// </summary>
        /// <returns></returns>
        IUnitOfWork Create();
    }
}
