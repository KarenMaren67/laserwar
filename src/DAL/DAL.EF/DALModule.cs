using Application.Abstraction.DAL;
using Prism.Ioc;
using Prism.Modularity;

namespace DAL.EF
{
    public class DALModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IConnectionStringProvider<string>, PostgreSqlConnectionStringProvider>();
            containerRegistry.Register<IUnitOfWorkFactory, EFUnitOfWorkFactory>();
        }
    }
}
