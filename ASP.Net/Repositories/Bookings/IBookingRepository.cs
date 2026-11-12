using SmartAirPort.Models;
using SmartAirPort.Repositories.Base;

namespace SmartAirPort.Repositories.Bookings
{
    public interface IBookingRepository : IRepository<Booking>
    {
        IEnumerable<Booking> GetBookingsWithFlightDetails();

        IEnumerable<Booking> FilterBookings(string? searchTerm, int? flightId, string? status);
    }
}