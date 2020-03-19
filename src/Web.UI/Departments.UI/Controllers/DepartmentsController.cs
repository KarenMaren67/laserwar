using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.Validation.Department;
using Departments.UI.Models;
using Departments.UI.Models.Departments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Departments.UI.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IService<Department> _departmentsService;

        public DepartmentsController(IService<Department> departmentsService)
        {
            _departmentsService = departmentsService;
        }

        // GET: Departments
        public ActionResult Index()
        {
            var model = new MainViewModel(BuildTree(), null);
            return View(model);
        }

        private IEnumerable<DepartmentsTreeItemViewModel> BuildTree(int? selectedId = null)
        {
            var rootDepartments = _departmentsService.GetAllWhere(x => !x.ParentDepartmentId.HasValue);

            foreach (var rootDept in rootDepartments)
            {
                var rootDeptVM = new DepartmentsTreeItemViewModel(rootDept.Id.Value, rootDept.Name, null, null, selectedId.HasValue && rootDept.Id.Value == selectedId.Value);
                rootDeptVM.ChildDepartments = GetChildsRecoursive(rootDeptVM).ToList();
                yield return rootDeptVM;
            }

            //Метод рекурсивного заполнения родительских элементов дерева потомками
            IEnumerable<DepartmentsTreeItemViewModel> GetChildsRecoursive(DepartmentsTreeItemViewModel parentVM)
            {
                var childDepartments = _departmentsService.GetAllWhere(x => x.ParentDepartmentId == parentVM.Id);
                foreach (var child in childDepartments)
                {
                    var childDeptVM = new DepartmentsTreeItemViewModel(
                        child.Id.Value,
                        child.Name,
                        null,
                        parentVM,
                        selectedId.HasValue && child.Id.Value == selectedId.Value);

                    childDeptVM.ChildDepartments = GetChildsRecoursive(childDeptVM).ToList();

                    if (selectedId.HasValue && childDeptVM.Id == selectedId.Value)
                    {
                        setSelectedRecursive(childDeptVM);
                    }

                    yield return childDeptVM;
                }
            }
        }

        private void setSelectedRecursive(DepartmentsTreeItemViewModel dept)
        {
            if (dept != null)
            {
                dept.IsSelected = true;
                setSelectedRecursive(dept.Parent);
            }
        }


        // GET: Departments/Details/5
        public ActionResult Details(int id)
        {
            var model = new MainViewModel(BuildTree(id), null);
            return View("Index", model);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Departments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}