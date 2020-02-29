using Application.Abstraction.DAL;
using Application.Validation.Department;
using AutoMapper;
using Domain.DbEntities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Application.Services
{
    public class DepartmentsService : IService<Department>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly AbstractValidator<Department> _departmentValidator;

        public DepartmentsService(IUnitOfWorkFactory unitOfWorkFactory, 
            AbstractValidator<Department> departmentValidator, 
            IMapper mapper)
        {
            _departmentValidator = departmentValidator;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public Department Add(Department department)
        {
            if (!_departmentValidator.Validate(department).IsValid)
            {
                throw new InvalidOperationException("Error. No valid department");
            }
            using (var _unitOfWork = _unitOfWorkFactory.Create())
            {
                _unitOfWork.Departments.Insert(department);
                _unitOfWork.Save();
            }

            return department;
        }

        public Department Edit(Department department)
        {
            if (!_departmentValidator.Validate(department).IsValid || !department.Id.HasValue)
            {
                throw new InvalidOperationException("Error. No valid department");
            }

            using (var _unitOfWork = _unitOfWorkFactory.Create())
            {
                _unitOfWork.Departments.Update(department);
                _unitOfWork.Save();
            }

            return department;
        }

        public void Delete(int id)
        {
            using (var _unitOfWork = _unitOfWorkFactory.Create())
            {
                _unitOfWork.Departments.Delete(id);
                _unitOfWork.Save();
            }
        }

        public IEnumerable<Department> GetAll()
        {
            using (var _unitOfWork = _unitOfWorkFactory.Create())
            {
                return _unitOfWork.Departments.GetAll().ToList();
            }
        }

        public Department Get(int id)
        {
            using (var _unitOfWork = _unitOfWorkFactory.Create())
            {
                return _unitOfWork.Departments.Get(id);
            }
        }

        public IEnumerable<Department> GetAllWhere(Expression<Func<Department, bool>> exp)
        {
            using (var _unitOfWork = _unitOfWorkFactory.Create())
            {
                return _unitOfWork.Departments.GetAll().Where(exp).ToList();
            }
        }
    }
}
