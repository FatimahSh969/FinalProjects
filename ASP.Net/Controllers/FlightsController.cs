
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartAirPort.Data;
using SmartAirPort.Models;
using SmartAirPort.Repositories.Base;
using Microsoft.AspNetCore.Mvc.Rendering;


public class FlightsController : Controller
{

    private readonly IUnitOfWork _unitOfWork;
    public FlightsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public IActionResult Index(string? status,int? airlineId,int? aircraftId)
    {
        loadFlightsDropdowns();

        ViewBag.SelectedStatus = status;
        ViewBag.SelectedAirlineId = airlineId;
        ViewBag.SelectedAircraftId = aircraftId;

        var flights = _unitOfWork.Flights.FilterFlights(status, airlineId, aircraftId);

        return View(flights);
    }




    [HttpGet]
    public IActionResult Details(int id)
    {
        var flight = _unitOfWork.Flights
            .GetFlightsWithDetails()
            .FirstOrDefault(f => f.Id == id);

        if (flight == null)
        {
            return NotFound();
        }

        return View(flight);
    }


    [HttpGet]
    public IActionResult Create()
    {
        loadFlightsDropdowns();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Flight flight)
    {
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState)
            {
                foreach (var item in error.Value.Errors)
                {
                    Console.WriteLine($"{error.Key}: {item.ErrorMessage}");
                }
            }

            loadFlightsDropdowns();
            return View(flight);
        }

        _unitOfWork.Flights.Add(flight);

        return RedirectToAction(nameof(Index));
    }
    public IActionResult Edit(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var flight = _unitOfWork.Flights.GetById(id);
        if (flight == null)
        {
            return NotFound();
        }
        loadFlightsDropdowns();
        return View(flight);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Flight flight)
    {
        var existingFlight = _unitOfWork.Flights.GetById(flight.Id);

        if (existingFlight == null)
            return NotFound();

        if (!ModelState.IsValid)
        {
            loadFlightsDropdowns();
            return View(flight);
        }

        string oldStatus = existingFlight.Status;

        existingFlight.FlightNumber = flight.FlightNumber;
        existingFlight.ArrivalAirport = flight.ArrivalAirport;
        existingFlight.DepartureAirport = flight.DepartureAirport;
        existingFlight.DepartureTime = flight.DepartureTime;
        existingFlight.ArrivalTime = flight.ArrivalTime;
        existingFlight.Status = flight.Status;
        existingFlight.AirlineId = flight.AirlineId;
        existingFlight.AircraftId = flight.AircraftId;
        existingFlight.GateId = flight.GateId;

        _unitOfWork.Flights.Update(existingFlight);

        if (oldStatus != flight.Status)
        {
            if (flight.Status == "Delayed")
            {
                CreateFlightNotification(
                    flight.Id,
                    "Flight Delayed",
                    $"Flight {flight.FlightNumber} has been delayed.",
                    "Delayed"
                );
            }
            else if (flight.Status == "Cancelled")
            {
                CreateFlightNotification(
                    flight.Id,
                    "Flight Cancelled",
                    $"Flight {flight.FlightNumber} has been cancelled.",
                    "Cancelled"
                );
            }
        }

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var flight = _unitOfWork.Flights.GetById(id);
        if (flight == null)
        {
            return NotFound();
        }

        return View(flight);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var flight = _unitOfWork.Flights.GetById(id);
        if (flight != null)
        {
            _unitOfWork.Flights.Delete(flight);
        }

        return RedirectToAction(nameof(Index));
    }

    //private bool FlightExists(int? id)
    //{
    //    return _context.Flights.Any(e => e.Id == id);
    //}
    private void loadFlightsDropdowns()
    {
        IEnumerable<Airline> airlines = _unitOfWork.Airlines.GetAll();
        ViewBag.AirlineList = new SelectList(airlines, "Id", "Name");

        IEnumerable<Aircraft> aircrafts = _unitOfWork.Aircraft.GetAll();
        ViewBag.AircraftList = new SelectList(aircrafts, "Id", "RegistrationNumber");

        IEnumerable<Gate> gates = _unitOfWork.Gates.GetAll();
        ViewBag.GateList = new SelectList(gates, "Id", "GateNumber");
    }


    private void CreateFlightNotification(int flightId, string title, string message, string type)
    {
        var notification = new Notification
        {
            FlightId = flightId,
            Title = title,
            Message = message,
            Type = type,
            CreatedAt = DateTime.Now,
            IsRead = false
        };

        _unitOfWork.Notifications.Add(notification);
    }



}
