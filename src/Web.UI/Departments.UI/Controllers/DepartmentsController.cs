using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.Validation.Department;
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
            var parentDepartments = _departmentsService.GetAllWhere(x => !x.ParentDepartmentId.HasValue);
            var model = new List<DepartmentsTreeItemViewModel>();

            foreach (var dept in parentDepartments.Where(x => !x.ParentDepartmentId.HasValue))
            {
                model.Add(new DepartmentsTreeItemViewModel(dept.Id.Value, dept.Name, GetChildsRecoursive(dept.Id.Value))); 
            }

            return View(model);

            //Метод рекурсивного заполнения родительских элементов дерева потомками
            List<DepartmentsTreeItemViewModel> GetChildsRecoursive(int id)
            {
                var childDepartments = _departmentsService.GetAllWhere(x => x.ParentDepartmentId == id);
                var result = new List<DepartmentsTreeItemViewModel>();
                if (childDepartments != null)
                {
                    foreach (var dept in childDepartments)
                    {
                        result.Add(new DepartmentsTreeItemViewModel(dept.Id.Value, dept.Name, GetChildsRecoursive(dept.Id.Value)));
                    }
                }

                return result;
            }
        }

        // GET: Departments/Details/5
        public ActionResult Details(int id)
        {
            return View();
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