
using Microsoft.EntityFrameworkCore;
using SmartAirPort.Data;
using SmartAirPort.Models;
using SmartAirPort.Repositories.Base;

namespace SmartAirPort.Repositories.Bookings
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(AirPortDBContext db) : base(db)
        {

        }

        public IEnumerable<Booking> GetBookingsWithFlightDetails()
        {
            return _context.Bookings
                .Include(b => b.Flight)
                    .ThenInclude(f => f.Airline)
                .ToList();
        }

        public IEnumerable<Booking> FilterBookings(
            string? searchTerm,
            int? flightId,
            string? status)
        {
            var query = _context.Bookings
                .Include(b => b.Flight)
                    .ThenInclude(f => f.Airline)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(b =>
                    b.BookingReference.Contains(searchTerm) ||
                    b.PassengerName.Contains(searchTerm) ||
                    b.PassengerIdNumber.Contains(searchTerm));
            }

            if (flightId.HasValue)
            {
                query = query.Where(b =>
                    b.FlightId == flightId.Value);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(b =>
                    b.Status == status);
            }

            return query
                .OrderByDescending(b => b.BookingDate)
                .ToList();
        }
    }
}
