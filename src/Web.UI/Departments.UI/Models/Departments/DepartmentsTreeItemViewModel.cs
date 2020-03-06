using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Departments.UI.Models.Departments
{
    public class DepartmentsTreeItemViewModel
    {
        public DepartmentsTreeItemViewModel(int id, string name, List<DepartmentsTreeItemViewModel> childsList)
        {
            Id = id;
            Name = name;
            ChildDepartments = childsList ?? new List<DepartmentsTreeItemViewModel>();
        }

        public int Id { get; }

        public string Name { get;  }

        public List<DepartmentsTreeItemViewModel> ChildDepartments { get;  }
    }
}
