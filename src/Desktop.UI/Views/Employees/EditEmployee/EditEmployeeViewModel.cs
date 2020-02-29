using Application.Enums;
using Application.Extensions;
using Application.Services;
using Application.Validation.Emplyee;
using FluentValidation;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;

namespace Departments.Views.Employees.EditEmployee
{
    public class EditEmployeeViewModel : BindableBase, INavigationAware
    {
        private readonly IApplicationStateManager _applicationStateManager;
        private readonly IService<Employee> _employeesService;
        private readonly AbstractValidator<Employee> _employeeValidator;
        private int _parentDepartmentId;
        private string _name;
        private string _selectedPosition;
        private string _phone;
        private Stack<int> _navigationStack;
        private Employee _employee;

        public EditEmployeeViewModel(IApplicationStateManager applicationStateManager,
        IService<Employee> employeesService,
        AbstractValidator<Employee> employeeValidator)
        {
            _applicationStateManager = applicationStateManager;
            _employeesService = employeesService;
            _employeeValidator = employeeValidator;
            PositionsNames = typeof(Positions).GetEnumNames();
            SaveCommand = new DelegateCommand(OnSave, CanSave);
            SaveCommand.ObservesProperty(() => Name);
            SaveCommand.ObservesProperty(() => SelectedPosition);
            CancelCommand = new DelegateCommand(OnCancel);
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string[] PositionsNames { get; }
        public string SelectedPosition
        {
            get => _selectedPosition;
            set => SetProperty(ref _selectedPosition, value);
        }

        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value);
        }

        public DelegateCommand SaveCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _parentDepartmentId = navigationContext.Parameters
                                   .TryGetValue<int>("DepartmentId", out var departmentId)
                                   ? departmentId
                                   : throw new ArgumentNullException("Отсутствует идентификатор подразделения!");

            _navigationStack = navigationContext.Parameters
                                .TryGetValue<Stack<int>>("NavigationStack", out var stack)
                                ? stack
                                : new Stack<int>();

            var employeeId = navigationContext.Parameters
                                .TryGetValue<int>("EmployeeId", out var id)
                                ? id
                                : throw new ArgumentNullException("Отсутствует идентификатор сотрудника!");

            _employee = _employeesService.Get(employeeId);

            GetFieldsValuesFromEmployee();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
           
        }

        private bool CanSave() => !Name.IsNullOrWhiteSpace()
                                && !SelectedPosition.IsNullOrWhiteSpace()
                                && SelectedPosition != Positions.None.ToString();

        private void OnSave()
        {
            SetFieldsValuestoEmployee();

            if (_employeeValidator.Validate(_employee).IsValid)
            {
                _employeesService.Edit(_employee);
                GoBack();
            }
        }
        private void OnCancel()
        {
            GoBack();
        }
        private void GoBack()
        {
            var parentDepartmentId = _navigationStack.Pop();
            _applicationStateManager.ChangeStateRequest("DepartmentDetails", GetParams(parentDepartmentId));
        }

        private NavigationParameters GetParams(int parentDepartmentId)
        {
            var parameters = new NavigationParameters();

            parameters.Add("DepartmentId", parentDepartmentId);
            parameters.Add("NavigationStack", _navigationStack);

            return parameters;
        }

        private void GetFieldsValuesFromEmployee()
        {
            this.Name = _employee.Name;
            this.SelectedPosition = _employee.Position;
            this.Phone = _employee.Phone;
        }

        private void SetFieldsValuestoEmployee()
        {
            _employee.Name = this.Name;
            _employee.Position = this.SelectedPosition;
            _employee.Phone = this.Phone;
            _employee.ParentDepartmentId = _parentDepartmentId;
        }
    }
}
