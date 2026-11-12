
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartAirPort.Models;
using SmartAirPort.Repositories.Base;

namespace SmartAirPort.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index(string? searchTerm, int? flightId, string? status)
        {
            var bookings = _unitOfWork.Bookings.FilterBookings(
                searchTerm,
                flightId,
                status
            );

            var flights = _unitOfWork.Flights.GetAll()
                .Select(f => new
                {
                    f.Id,
                    Display = f.FlightNumber + " - " +
                              f.DepartureAirport + " to " +
                              f.ArrivalAirport
                })
                .ToList();

            ViewBag.FlightId = new SelectList(
                flights,
                "Id",
                "Display",
                flightId
            );

            ViewBag.Status = new SelectList(
                new List<string> { "Confirmed", "Cancelled" },
                status
            );

            ViewBag.SearchTerm = searchTerm;

            return View(bookings);
        }


        public IActionResult Create()
        {
            LoadFlights();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Booking booking)
        {
            var flight = _unitOfWork.Flights.GetById(booking.FlightId);

            if (flight == null || flight.Status == "Cancelled")
            {
                ModelState.AddModelError(
                    "FlightId",
                    "Please select a valid, non-cancelled flight.");
            }

            if (ModelState.IsValid)
            {
                booking.BookingReference =
                    "BK" + Guid.NewGuid().ToString("N")
                    .Substring(0, 8).ToUpper();

                booking.BookingDate = DateTime.Now;
                booking.Status = "Confirmed";

                _unitOfWork.Bookings.Add(booking);

                return RedirectToAction(nameof(Index));
            }

            LoadFlights(booking.FlightId);
            return View(booking);
        }

        public IActionResult Edit(int id)
        {
            var booking = _unitOfWork.Bookings.GetById(id);

            if (booking == null)
                return NotFound();

            LoadFlights(booking.FlightId);
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Booking booking)
        {
            var existingBooking = _unitOfWork.Bookings.GetById(id);

            if (existingBooking == null)
                return NotFound();

            var flight = _unitOfWork.Flights.GetById(booking.FlightId);

            if (flight == null || flight.Status == "Cancelled")
            {
                ModelState.AddModelError(
                    "FlightId",
                    "Please select a valid, non-cancelled flight.");
            }

            if (ModelState.IsValid)
            {
                existingBooking.PassengerName = booking.PassengerName;
                existingBooking.PassengerIdNumber = booking.PassengerIdNumber;
                existingBooking.ContactNumber = booking.ContactNumber;
                existingBooking.FlightId = booking.FlightId;

                _unitOfWork.Bookings.Update(existingBooking);

                return RedirectToAction(nameof(Index));
            }

            booking.Id = id;
            LoadFlights(booking.FlightId);
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(int id)
        {
            var booking = _unitOfWork.Bookings.GetById(id);

            if (booking == null)
                return NotFound();

            booking.Status = "Cancelled";

            _unitOfWork.Bookings.Update(booking);

            return RedirectToAction(nameof(Index));
        }

        private void LoadFlights(int? selectedFlightId = null)
        {
            var flights = _unitOfWork.Flights.GetAll()
                .Where(f => f.Status != "Cancelled")
                .Select(f => new
                {
                    f.Id,
                    Display = f.FlightNumber + " - " +
                              f.DepartureAirport + " to " +
                              f.ArrivalAirport
                })
                .ToList();

            ViewBag.FlightId = new SelectList(
                flights,
                "Id",
                "Display",
                selectedFlightId
            );
        }
    }
}
