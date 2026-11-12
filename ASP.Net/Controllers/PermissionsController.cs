using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartAirPort.Models;
using SmartAirPort.Repositories.Base;
using SmartAirPort.Security;

namespace SmartAirPort.Controllers
{
    [Authorize]
    public class PermissionsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PermissionsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = PermissionsNames.PermissionManagement)]

        public IActionResult Index()
        {
            return View(_unitOfWork.Permissions.GetAll());
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Permission permission = _unitOfWork.Permissions.GetById(id);

            return View(permission);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadRoles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name")] Permission permission)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Permissions.Add(permission);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(permission);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            var permission = _unitOfWork.Permissions.GetById(id);

            if (permission == null)
                return NotFound();
            LoadRoles();
            return View(permission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind("Id,Name")] Permission permission)
        {
            if (id != permission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Permissions.Update(permission);
                    _unitOfWork.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!PermissionExists(permission.Id))
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
            return View(permission);
        }

        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Permission permission = _unitOfWork.Permissions.GetById(id);
            if (permission == null)
            {
                return NotFound();
            }

            return View(permission);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Permission permission = _unitOfWork.Permissions.GetById(id);
            if (permission != null)
            {
                _unitOfWork.Permissions.Delete(permission);
            }

            _unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //private bool PermissionExists(int? id)
        //{
        //    return _unitOfWork.Permissions.Any(e => e.Id == id);
        //}
        private void LoadRoles()
        {
            ViewBag.AllRoles = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _unitOfWork.Roles.GetAll(),
                "Id",
                "Name");
        }
    }
}