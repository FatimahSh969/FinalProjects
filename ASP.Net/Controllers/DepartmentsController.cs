
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartAirPort.Models;
using SmartAirPort.Repositories.Base;
using SmartAirPort.Security;


namespace SmartAirPort.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize(Policy = PermissionsNames.DepartmentManagement)]

        public IActionResult Index()
        {
            return View(_unitOfWork.Departments.GetAll());
        }

        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _unitOfWork.Departments.GetById(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Departments.Add(department);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var department = _unitOfWork.Departments.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            loadDepartmentEmployees(department);
            return View(department);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Departments.Update(department);
                    _unitOfWork.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!DepartmentExists(department.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = _unitOfWork.Departments.GetById(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var department = _unitOfWork.Departments.GetById(id);
            if (department != null)
            {
                _unitOfWork.Departments.Delete(department);
                _unitOfWork.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
        private void loadDepartmentEmployees(Department department)
        {
            ViewBag.EmployeeNames = department.Employees?
                .Select(e => e.Name)
                .ToList() ?? new List<string>();
        }

        //private bool DepartmentExists(int? id)
        //{
        //    return _context.Departments.Any(e => e.Id == id);
        //}
    }
}