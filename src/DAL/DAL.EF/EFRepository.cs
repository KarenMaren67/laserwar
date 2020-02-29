using Application.Abstraction.DAL;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccessLayer.DAL.EF
{
    class EFRepository<TBusinessEntity, TStorageEntity> : IRepository<TBusinessEntity, TStorageEntity> 
        where TStorageEntity : AbstractEntity 
        where TBusinessEntity : class
    {
        private readonly IMapper _mapper;
        private readonly DbContext _context;
        protected DbSet<TStorageEntity> Table => _context.Set<TStorageEntity>();

        public EFRepository(EFDataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Delete(int id)
        {
            var entity = _mapper.Map<TStorageEntity>(Table.First(x => x.Id.Equals(id)));
            Table.Remove(entity);
        }

        public IQueryable<TBusinessEntity> GetAll() => _mapper.ProjectTo<TBusinessEntity>(Table.AsQueryable());
        public TBusinessEntity Insert(TBusinessEntity entity)
        {
            Table.Add(_mapper.Map<TStorageEntity>(entity));
            return entity;
        }

        public TBusinessEntity Update(TBusinessEntity entity)
        {
            Table.Update(_mapper.Map<TStorageEntity>(entity));
            return entity;
        }

        public TBusinessEntity Get(int id) => Table.Any(x => x.Id.Equals(id)) 
            ? _mapper.Map<TBusinessEntity>(Table.First(x => x.Id.Equals(id))) 
            : null;
    }
}
