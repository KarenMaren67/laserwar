using Application.Abstraction.DAL;
using Application.Validation.Department;
using Application.Validation.Emplyee;
using AutoMapper;
using DataAccessLayer.DAL.EF;
using Domain.DbEntities;

namespace DAL.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly EFDataContext _db;
        public EFUnitOfWork(IConnectionStringProvider<string> connectionStringProvider, IMapper mapper)
        {
            _db = new EFDataContext(connectionStringProvider);
            Employees = new EFRepository<Employee, EmployeeEntity>(_db, mapper);
            Departments = new EFRepository<Department, DepartmentEntity>(_db, mapper);
        }

        public IRepository<Employee, EmployeeEntity> Employees { get; set; }
        public IRepository<Department, DepartmentEntity> Departments { get; set; }

        public void Dispose()
        {
            _db.Dispose();
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
