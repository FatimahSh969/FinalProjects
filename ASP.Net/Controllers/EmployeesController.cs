
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartAirPort.Models;
using SmartAirPort.Repositories.Base;
using SmartAirPort.Security;
using System.Net;

namespace SmartAirPort.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class EmployeesController : Controller

    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = PermissionsNames.EmployeeManagement)]
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _unitOfWork.Employees.GetAllEmployeesWithDepartment();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
           
            var employee = _unitOfWork.Employees.GetEmployeeWithDepartment(id);
            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            loadDepartments();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Create(Employee emp)
        {
            _unitOfWork.Employees.Add(emp);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            loadDepartments();
            return View(_unitOfWork.Employees.GetEmployeeWithDepartment(id));
        }

        [HttpPost]
        public IActionResult Update(Employee updatedEmp)
        {
            _unitOfWork.Employees.Update(updatedEmp);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_unitOfWork.Employees.GetById(id));
        }

        [HttpPost]
        public IActionResult Delete(Employee delEmp)
        { 
            _unitOfWork.Employees.Delete(delEmp);
            return RedirectToAction("Index");
        }    
        private void loadDepartments()
        {   
            IEnumerable<Department> departments = _unitOfWork.Departments.GetAll();
            ViewBag.DepartmentList = new SelectList(departments, "Id", "Name");
        }


    }
}