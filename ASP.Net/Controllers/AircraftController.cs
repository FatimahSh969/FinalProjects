
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartAirPort.Data;
using SmartAirPort.Models;
using SmartAirPort.Repositories.Base;

public class AircraftController : Controller
{

    private readonly IUnitOfWork _unitOfWork;
    public AircraftController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
        return View(_unitOfWork.Aircraft.GetAll());
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var aircraft = _unitOfWork.Aircraft.GetById(id);

        if (aircraft == null)
        {
            return NotFound();
        }

        return View(aircraft);
    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Aircraft aircraft)
    {
        if (ModelState.IsValid)
        {
           _unitOfWork.Aircraft.Add(aircraft);

            return RedirectToAction(nameof(Index));
        }

        return View(aircraft);
    }
    public IActionResult Edit(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var aircraft = _unitOfWork.Aircraft.GetById(id);

        if (aircraft == null)
        {
            return NotFound();
        }

        return View(aircraft);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Aircraft aircraft)
    {
        _unitOfWork.Aircraft.Update(aircraft);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var aircraft =_unitOfWork.Aircraft.GetById(id);

        if (aircraft == null)
        {
            return NotFound();
        }

        return View(aircraft);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var aircraft = _unitOfWork.Aircraft.GetById(id);

        if (aircraft != null)
        {
            _unitOfWork.Aircraft.Delete(aircraft);
        }

        return RedirectToAction(nameof(Index));
    }

    //private bool AircraftExists(int id)
    //{
    //    return _context.Aircrafts.Any(a => a.Id == id);
    //}

}

