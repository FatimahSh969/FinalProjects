
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartAirPort.Data;
using SmartAirPort.Models;
using SmartAirPort.Repositories.Base;

public class GatesController : Controller
{
    //private readonly AirPortDBContext _context;

    private readonly IUnitOfWork _unitOfWork;
    public GatesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()    
    {
        return View(_unitOfWork.Gates.GetAll());
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        //if (id == null)
        //{
        //    return NotFound();
        //}

        //var gate = _repository.GetById(id);
        //if (gate == null)
        //{
        //    return NotFound();
        //}

        return View(_unitOfWork.Gates.GetById(id));
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Gate gate)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Gates.Add(gate);
            return RedirectToAction(nameof(Index));
        }
        return View(gate);
    }

    public IActionResult Edit(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var gate = _unitOfWork.Gates.GetById(id);
        if (gate == null)
        {
            return NotFound();
        }
        return View(gate);
    }

   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Gate gate)
    {
            _unitOfWork.Gates.Update(gate);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var gate = _unitOfWork.Gates.GetById(id);
        if (gate == null)
        {
            return NotFound();
        }

        return View(gate);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var gate = _unitOfWork.Gates.GetById(id);
        if (gate != null)
        {
            _unitOfWork.Gates.Delete(gate);
        }

        return RedirectToAction(nameof(Index));
    }

    //private bool GateExists(int? id)
    //{
    //    return _context.Gates.Any(e => e.Id == id);
    //}
}
