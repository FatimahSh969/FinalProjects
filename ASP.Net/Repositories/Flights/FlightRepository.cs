using Microsoft.EntityFrameworkCore;
using SmartAirPort.Data;
using SmartAirPort.Models;
using SmartAirPort.Repositories.Base;

namespace SmartAirPort.Repositories.Flights
{
    public class FlightRepository : Repository<Flight>, IFlightRepository
    {
        public FlightRepository(AirPortDBContext db) : base(db)
        {

        }

        public IEnumerable<Flight> GetFlightsWithDetails()
        {
            return _context.Flights
                .Include(f => f.Airline)
                .Include(f => f.Aircraft)
                .Include(f => f.Gate)
                .ToList();
        }
        public IEnumerable<Flight> FilterFlights(string? status,int? airlineId,int? aircraftId)
        {
            var query = _context.Flights
                .Include(f => f.Airline)
                .Include(f => f.Aircraft)
                .Include(f => f.Gate)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(f => f.Status == status);
            }

            if (airlineId.HasValue)
            {
                query = query.Where(f => f.AirlineId == airlineId.Value);
            }

            if (aircraftId.HasValue)
            {
                query = query.Where(f => f.AircraftId == aircraftId.Value);
            }

            return query.ToList();
        }
    }
}
    
