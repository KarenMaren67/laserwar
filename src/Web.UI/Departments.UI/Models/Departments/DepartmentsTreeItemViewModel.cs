using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Departments.UI.Models.Departments
{
    public class DepartmentsTreeItemViewModel
    {
        public DepartmentsTreeItemViewModel(int id, string name, IEnumerable<DepartmentsTreeItemViewModel> childsList, DepartmentsTreeItemViewModel parent, bool isSelected = false)
        {
            Id = id;
            Name = name;
            ChildDepartments = childsList ?? new List<DepartmentsTreeItemViewModel>();
            Parent = parent;
            IsSelected = isSelected;
        }

        public int Id { get; }

        public string Name { get;  }

        public IEnumerable<DepartmentsTreeItemViewModel> ChildDepartments { get; set;}

        public DepartmentsTreeItemViewModel Parent { get; set; }

        public bool IsSelected { get; set;}
    }
}
