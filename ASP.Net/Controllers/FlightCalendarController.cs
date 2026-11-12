
using Microsoft.AspNetCore.Mvc;
using SmartAirPort.Repositories.Base;

namespace SmartAirPort.Controllers
{
    public class FlightCalendarController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FlightCalendarController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetFlightEvents()
        {
            var flights = _unitOfWork.Flights
                .GetFlightsWithDetails()
                .ToList();

            var events = flights.Select(flight => new
            {
                id = flight.Id,

                title = flight.FlightNumber +
                    " - " + (flight.Airline?.Name ?? "N/A"),

                start = flight.DepartureTime,
                end = flight.ArrivalTime,

                backgroundColor =
                    flight.Status == "Delayed" ? "#dc3545" :
                    flight.Status == "Cancelled" ? "#6c757d" :
                    flight.Status == "Boarding" ? "#ffc107" :
                    flight.Status == "Departed" ? "#0dcaf0" :
                    flight.Status == "Arrived" ? "#198754" :
                    "#0d6efd",

                extendedProps = new
                {
                    status = flight.Status,
                    departureAirport = flight.DepartureAirport,
                    arrivalAirport = flight.ArrivalAirport
                }
            });

            return Json(events);
        }
    }
}
