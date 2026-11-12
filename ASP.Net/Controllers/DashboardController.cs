using Microsoft.AspNetCore.Mvc;
using SmartAirPort.Models.ViewModels;
using SmartAirPort.Repositories.Base;

namespace SmartAirPort.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var flights = _unitOfWork.Flights
                .GetFlightsWithDetails()
                .OrderBy(f => f.DepartureTime)
                .ToList();

            var model = new DashboardViewModel
            {
                TotalFlights = flights.Count,

                DelayedFlights = flights.Count(f =>
                    f.Status == "Delayed"),

                TotalAircraft = _unitOfWork.Aircraft.GetAll().Count(),

                TotalAirlines = _unitOfWork.Airlines.GetAll().Count(),

                TotalEmployees = _unitOfWork.Employees.GetAll().Count(),

                AvailableGates = _unitOfWork.Gates.GetAll().Count(g =>
                    g.IsActive && g.Status == "Available"),

                UpcomingFlights = flights
                    .Where(f => f.DepartureTime >= DateTime.Now)
                    .Take(5)
                    .ToList(),

                FlightStatuses = flights
                    .GroupBy(f => f.Status)
                    .Select(g => new FlightStatusSummary
                    {
                        Status = g.Key,
                        Count = g.Count()
                    })
                    .ToList(),

                FlightsByAirline = flights
                    .GroupBy(f => f.Airline != null
                        ? f.Airline.Name
                        : "Unknown")
                    .Select(g => new AirlineFlightSummary
                    {
                        AirlineName = g.Key,
                        FlightCount = g.Count()
                    })
                    .OrderByDescending(x => x.FlightCount)
                    .ToList()
            };

            return View(model);
        }


    }
}