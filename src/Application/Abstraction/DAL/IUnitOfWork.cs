using Application.Validation.Department;
using Application.Validation.Emplyee;
using Domain.DbEntities;
using System;

namespace Application.Abstraction.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Employee, EmployeeEntity> Employees { get; set; }
        IRepository<Department, DepartmentEntity> Departments { get; set; }
        void Save();
    }

}
