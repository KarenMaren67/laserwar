using Application.Services;
using Application.Validation.Department;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Linq;

namespace Departments.Views.Main
{
    class MainViewModel : BindableBase, INavigationAware
    {
        private readonly IApplicationStateManager _applicationStateManager;
        private readonly IService<Department> _departmentsService;

        public MainViewModel(IApplicationStateManager applicationStateManager,
            IService<Department> departmentsService)
        {
            _applicationStateManager = applicationStateManager;
            _departmentsService = departmentsService;
            ViewDepartmentDetailsCommand = new DelegateCommand<Department>(OnViewDepartmentDetails);
        }

        public DelegateCommand<Department> ViewDepartmentDetailsCommand { get; set; }

        public IReadOnlyList<Department> Departments { get; private set; }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Departments = _departmentsService.GetAll().Where(x => x.ParentDepartmentId == null).OrderBy(x => x.Name).ToList();
            RaisePropertyChanged("Departments");
        }

        private void OnViewDepartmentDetails(Department department) 
        {
            var parameters = new NavigationParameters();
            parameters.Add("DepartmentId", department.Id);
            _applicationStateManager.ChangeStateRequest("DepartmentDetails", parameters);
        }
    }
}
