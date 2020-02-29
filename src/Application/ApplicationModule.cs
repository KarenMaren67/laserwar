using Prism.Modularity;
using Prism.Ioc;
using Application.Services;
using Application.Validation.Department;
using FluentValidation;
using Application.Validation.Emplyee;
using Domain.DbEntities;

namespace Application
{
    public class ApplicationModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IService<Department>, DepartmentsService>();
            containerRegistry.Register<IService<Employee>, EmployeeService>();
            containerRegistry.Register<AbstractValidator<Department>, DepartmentValidator>();
            containerRegistry.Register<AbstractValidator<Employee>, EmployeeValidator>();
        }
    }
}
