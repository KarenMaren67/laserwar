using Departments.UI.Models.Departments;
using Departments.UI.Models.Employees;
using System.Collections.Generic;

namespace Departments.UI.Models
{
    public class MainViewModel
    {
        public MainViewModel(IEnumerable<DepartmentsTreeItemViewModel> treeItems, EmployeesListViewModel employees)
        {
            TreeItems = treeItems;
            Employees = employees;
        }

        public IEnumerable<DepartmentsTreeItemViewModel> TreeItems { get; set;}

        public EmployeesListViewModel Employees { get; set;}
    }
}
