using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartAirPort.Data;
using SmartAirPort.Models;
using SmartAirPort.Repositories.Base;

namespace SmartAirPort.Controllers
{
    public class AirlinesController : Controller
    {
        //private readonly AirPortDBContext _context;
        //private readonly IRepository<Airline> _repository;
        private readonly IUnitOfWork _unitOfWork;
        public AirlinesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View(_unitOfWork.Airlines.GetAll());
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var airline = _repository.GetById(id);

            //if (airline == null)
            //{
            //    return NotFound();
            //}

            return View(_unitOfWork.Airlines.GetById(id));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Airline airline)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Airlines.Add(airline);
                return RedirectToAction(nameof(Index));
            }

            return View(airline);
        }

        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airline =_unitOfWork.Airlines.GetById(id);

            if (airline == null)
            {
                return NotFound();
            }

            return View(airline);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Airline airline)
        {
            _unitOfWork.Airlines.Update(airline);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airline = _unitOfWork.Airlines.GetById(id);

            if (airline == null)
            {
                return NotFound();
            }

            return View(airline);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var airline = _unitOfWork.Airlines.GetById(id);

            if (airline != null)
            {
                _unitOfWork.Airlines.Delete(airline);
            }

            return RedirectToAction(nameof(Index));
        }

        //private bool AirlineExists(int id)
        //{
        //    return _context.Airlines.Any(a => a.Id == id);
        //}


    }


}