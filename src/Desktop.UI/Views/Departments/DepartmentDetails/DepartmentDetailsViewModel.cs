using Application.Services;
using Application.Validation.Department;
using Application.Validation.Emplyee;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Departments.Views.Departments.DepartmentDetails
{
    public class DepartmentDetailsViewModel : BindableBase, INavigationAware 
    {
        private readonly IApplicationStateManager _applicationStateManager;
        private readonly IService<Department> _departmentsService;
        private readonly IService<Employee> _employeesService;
        private readonly ObservableCollection<Employee> _employees;
        private Department _department;
        private bool _hasEmployees;
        private Stack<int> _navigationStack;

        public DepartmentDetailsViewModel(IApplicationStateManager applicationStateManager, 
            IService<Department> departmentsService,
            IService<Employee> employeesService)
        {
            _applicationStateManager = applicationStateManager;
            _departmentsService = departmentsService;
            _employeesService = employeesService;
            _employees = new ObservableCollection<Employee>();
            ViewDepartmentDetailsCommand = new DelegateCommand<Department>(OnViewDepartmentDetails);
            GoBackCommand = new DelegateCommand(OnGoBack);
            AddEmloyeeCommand = new DelegateCommand(OnAddEmployee);
            EditEmployeeCommand = new DelegateCommand<int?>(OnEditEmployee);
            DeleteEmployeeCommand = new DelegateCommand<int?>(OnDeleteEmployee);
        }

        public bool HasEmployees
        {
            get => _hasEmployees;
            set => SetProperty(ref _hasEmployees, value);
        }

        public string DepartmentName { get; private set; }

        public ObservableCollection<Employee> Employees { get => _employees; }

        public List<Department> Departments { get; private set; }

        
        public DelegateCommand<Department> ViewDepartmentDetailsCommand { get; set; }
        public DelegateCommand GoBackCommand { get; set; }

        public DelegateCommand AddEmloyeeCommand { get; set; }

        public DelegateCommand<int?> EditEmployeeCommand { get; set; }
        public DelegateCommand<int?> DeleteEmployeeCommand { get; set; }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var departmentId = navigationContext.Parameters
                                    .TryGetValue<int>("DepartmentId", out var deptId)
                                    ? deptId
                                    : throw new ArgumentNullException("Отсутствует идентификатор подразделения!");

            _navigationStack = navigationContext.Parameters
                                    .TryGetValue<Stack<int>>("NavigationStack", out var stack) 
                                    ? stack 
                                    : new Stack<int>();

            _department = _departmentsService.Get(departmentId);
            HasEmployees = _department.HasEmployees;
            DepartmentName = _department.Name;
            RaisePropertyChanged("DepartmentName");

            if (HasEmployees)
            {
                Departments = null;
                RenewEmployees();
            }
            else
            {
                var id = _department.Id.Value;
                Departments = _departmentsService.GetAllWhere(x => x.ParentDepartmentId == id).ToList();
                RaisePropertyChanged("Departments");
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }


        private void OnEditEmployee(int? employeeId)
        {
            _navigationStack.Push(this._department.Id.Value);
            var parameters = GetParamsForNavigation(_department.Id.Value);
            parameters.Add("EmployeeId", employeeId);
            _applicationStateManager.ChangeStateRequest("EditEmployee", parameters);
        }

        private void OnDeleteEmployee(int? employeeId)
        {
            if (employeeId.HasValue)
            {
                _employeesService.Delete(employeeId.Value);
            }
            RenewEmployees();
        }

        private void OnViewDepartmentDetails(Department department)
        {
            _navigationStack.Push(this._department.Id.Value);
            _applicationStateManager.ChangeStateRequest("DepartmentDetails", GetParamsForNavigation(department.Id.Value));
        }

        private void OnGoBack()
        {
            if (_navigationStack.Count > 0)
            {
                var department = _departmentsService.Get(_navigationStack.Pop());
                _applicationStateManager.ChangeStateRequest("DepartmentDetails", GetParamsForNavigation(department.Id.Value));
            }
            else 
            {
                _applicationStateManager.ChangeStateRequest("Main");
            }
        }

        private void OnAddEmployee()
        {
            _navigationStack.Push(this._department.Id.Value);
            _applicationStateManager.ChangeStateRequest("AddEmployee", GetParamsForNavigation(_department.Id.Value));
        }

        private NavigationParameters GetParamsForNavigation(int selectedDepartmentId) 
        {
            var parameters = new NavigationParameters();

            parameters.Add("DepartmentId", selectedDepartmentId);
            parameters.Add("NavigationStack", _navigationStack);

            return parameters;
        }

        private void RenewEmployees()
        {
            _employees.Clear();
            _employees.AddRange(_employeesService.GetAllWhere(x => x.ParentDepartmentId == _department.Id.Value).ToList());
            RaisePropertyChanged("Employees");
        }
    }
}
