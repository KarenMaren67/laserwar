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

namespace Departments.Views.Employees.AddEmployee
{
    class AddEmployeeViewModel : BindableBase, INavigationAware
    {
        private readonly AbstractValidator<Employee> _employeeValidator;
        private readonly IApplicationStateManager _applicationStateManager;
        private readonly IService<Employee> _employeesService;
        private int _parentDepartmentId;
        private string _name;
        private string _selectedPosition;
        private string _phone;
        private Stack<int> _navigationStack;

        public AddEmployeeViewModel(IService<Employee> employeesService, 
            AbstractValidator<Employee> employeeValidator,
            IApplicationStateManager applicationStateManager)
        {
            _applicationStateManager = applicationStateManager;
            _employeeValidator = employeeValidator;
            _employeesService = employeesService;
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

        public string[] PositionsNames { get; private set; }
        
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

        public DelegateCommand SaveCommand { get; set; }

        public DelegateCommand CancelCommand { get; set; }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Name = null;
            SelectedPosition = null;
            Phone = null;
        }

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
        }

        private bool CanSave() => !Name.IsNullOrWhiteSpace() 
                                && !SelectedPosition.IsNullOrWhiteSpace() 
                                && SelectedPosition != Positions.None.ToString();

        private void OnSave()
        {
            var employee = new Employee
            {
                Name = this.Name,
                Position = this.SelectedPosition,
                Phone = this.Phone,
                ParentDepartmentId = _parentDepartmentId
            };

            if (_employeeValidator.Validate(employee).IsValid)
            {
                _employeesService.Add(employee);
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
    }
}
