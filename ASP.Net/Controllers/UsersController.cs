
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartAirPort.Models;
using SmartAirPort.Repositories.Base;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class UsersController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    public UsersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        return View(_unitOfWork.Users.GetAll());
    }
    public IActionResult Details(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        User user = _unitOfWork.Users.GetById(id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Id,Name,Email,Username,Password,Roles")] User user)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Users.Add(user);
            _unitOfWork.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    public IActionResult Edit(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = _unitOfWork.Users.GetById(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int? id, [Bind("Id,Name,Email,Username,Password,Roles")] User user)
    {
        if (id != user.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _unitOfWork.Users.Update(user);
                _unitOfWork.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!UserExists(user.Id))
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
        return View(user);
    }

    public IActionResult Delete(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var user = _unitOfWork.Users.GetById(id);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        User user = _unitOfWork.Users.GetById(id);
        if (user != null)
        {
            _unitOfWork.Users.Delete(user);
        }

        _unitOfWork.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    //private bool UserExists(int? id)
    //{
    //    return _unitOfWork.Users.Any(e => e.Id == id);
    //}

    //[HttpGet]
    //public IActionResult AssignRoles(int UserId)
    //{
    //    User? user = _dbcontext.Users.Include(u => u.Roles).FirstOrDefault(u => u.Id == UserId);

    //    if (user == null)
    //    {
    //        return NotFound();
    //    }
    //    List<Role> roles = _dbcontext.Roles.ToList();

    //    ViewBag.AllRoles = roles;
    //    ViewBag.AssignedRoles = user.Roles?.Select(r => r.Id).ToList();
    //    return View(user);
    //}

    //[HttpPost]
    //public IActionResult AssignRoles(int UserId, List<int> RoleIds)
    //{

    //    User? user = _dbcontext.Users.Include(u => u.Roles).FirstOrDefault(u => u.Id == UserId);

    //    if (user == null)
    //    {
    //        return NotFound();
    //    }

    //    user.Roles.Clear();
    //    List<Role> selectedRoles = _dbcontext.Roles.Where(r => RoleIds.Contains(r.Id)).ToList();
    //    foreach (Role role in selectedRoles)
    //    {
    //        user.Roles.Add(role);

    //    }
    //    _dbcontext.SaveChanges();
    //    return RedirectToAction("Index");
    //}

}
