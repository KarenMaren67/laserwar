using Application.Abstraction.DAL;
using AutoMapper;

namespace DAL.EF
{
    public class EFUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IConnectionStringProvider<string> _connectionString;
        private readonly IMapper _mapper;

        public EFUnitOfWorkFactory(IConnectionStringProvider<string> connectionStringProvider, IMapper mapper)
        {
            _connectionString = connectionStringProvider;
            _mapper = mapper;
        }

        public IUnitOfWork Create() => new EFUnitOfWork(_connectionString, _mapper);
    }
}
