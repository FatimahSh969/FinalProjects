
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartAirPort.Data;
using SmartAirPort.Models;
using SmartAirPort.Repositories.Base;
using SmartAirPort.Security;

namespace SmartAirPort.Controllers
{

    [Authorize]
    public class RolesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public RolesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [Authorize(Policy = PermissionsNames.RoleManagement)]

        public IActionResult Index()
        {
            var roles = _unitOfWork.Roles.GetAllRolesWithDetails();
            return View(roles);
        }


        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = _unitOfWork.Roles.GetById(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Permissions,Users")] Role role)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Roles.Add(role);
                _unitOfWork.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = _unitOfWork.Roles.GetById(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, [Bind("Id,Name,Permissions,Users")] Role role)
        {
            if (id != role.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Roles.Update(role);
                    _unitOfWork.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!RoleExists(role.Id))
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
            return View(role);
        }

        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = _unitOfWork.Roles.GetById(id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var role = _unitOfWork.Roles.GetById(id);
            if (role != null)
            {
                _unitOfWork.Roles.Delete(role);
            }

            _unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //    private bool RoleExists(int? id)
        //    {
        //        return _dbcontext.Roles.Any(e => e.Id == id);
        //    }


        [HttpGet]
        public IActionResult AssignPermissions(int RoleId)
        {
            Role? role = _unitOfWork.Roles.GetRoleWithPermissions(RoleId);

            if (role == null)
            {
                return NotFound();
            }
            IEnumerable<Permission> permissions = _unitOfWork.Permissions.GetAll();

            ViewBag.AllPermissions = permissions;
            ViewBag.AssignedPermissions = role.Permissions?.Select(p => p.Id).ToList();

            return View(role);
        }

        [HttpPost]
        public IActionResult AssignPermissions(int RoleId, List<int> PermissionID)
        {
            Role? role = _unitOfWork.Roles.GetRoleWithPermissions(RoleId);

            if (role == null)
            {
                return NotFound();
            }
            role.Permissions.Clear();

            List<Permission> selectedPermissions = _unitOfWork.Permissions.GetAll().Where(p => PermissionID.Contains(p.Id)).ToList();

            foreach (Permission permission in selectedPermissions)
            {
                role.Permissions.Add(permission);
            }
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}