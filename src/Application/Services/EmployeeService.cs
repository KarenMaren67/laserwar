using Application.Abstraction.DAL;
using Application.Validation.Emplyee;
using AutoMapper;
using Domain.DbEntities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Application.Services
{
    public class EmployeeService : IService<Employee>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<Employee> _employeeValidator;

        public EmployeeService(IUnitOfWorkFactory UoWFactory, AbstractValidator<Employee> employeeValidator, IMapper mapper)
        {
            _employeeValidator = employeeValidator;
            _unitOfWorkFactory = UoWFactory;
            _mapper = mapper;
        }

        public Employee Add(Employee employee)
        {
            if (!_employeeValidator.Validate(employee).IsValid)
            {
                throw new InvalidOperationException("Error. No valid employee");
            }
            using (var _unitOfWork = _unitOfWorkFactory.Create())
            {
                _unitOfWork.Employees.Insert(employee);
                _unitOfWork.Save();
            }

            return employee;
        }

        public Employee Edit(Employee employee)
        {
            if (!_employeeValidator.Validate(employee).IsValid || !employee.Id.HasValue)
            {
                throw new InvalidOperationException("Error. No valid employee");
            }

            using (var _unitOfWork = _unitOfWorkFactory.Create())
            {
                _unitOfWork.Employees.Update(employee);
                _unitOfWork.Save();
            }
            return employee;
        }

        public void Delete(int id)
        {
            using (var _unitOfWork = _unitOfWorkFactory.Create())
            {
                _unitOfWork.Employees.Delete(id);
                _unitOfWork.Save();
            }
        }

        public IEnumerable<Employee> GetAll()
        {
            using (var _unitOfWork = _unitOfWorkFactory.Create())
            {
                return _unitOfWork.Employees.GetAll().ToList();
            }
        }

        public IEnumerable<Employee> GetAllWhere(Expression<Func<Employee, bool>> exp)
        {
            using (var _unitOfWork = _unitOfWorkFactory.Create())
            {
                return _unitOfWork.Employees.GetAll().Where(exp).ToList();
            }
        }

        public Employee Get(int id)
        {
            using (var _unitOfWork = _unitOfWorkFactory.Create())
            {
                return _unitOfWork.Employees.Get(id);
            }
        }
    }
}
