using SmartAirPort.Models;
using SmartAirPort.Repositories.Base;

namespace SmartAirPort.Repositories.Flights
{
    public interface IFlightRepository :IRepository<Flight>
    {
        IEnumerable<Flight> GetFlightsWithDetails();

        IEnumerable<Flight> FilterFlights(string? status,int? airlineId,int? aircraftId);
    }
}

