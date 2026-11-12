
using SmartAirPort.Models;

namespace SmartAirPort.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalFlights { get; set; }
        public int DelayedFlights { get; set; }
        public int TotalAircraft { get; set; }
        public int AvailableGates { get; set; }
        public int TotalAirlines { get; set; }
        public int TotalEmployees { get; set; }

        public List<Flight> UpcomingFlights { get; set; }
            = new List<Flight>();

        public List<FlightStatusSummary> FlightStatuses { get; set; }
            = new List<FlightStatusSummary>();

        public List<AirlineFlightSummary> FlightsByAirline { get; set; }
            = new List<AirlineFlightSummary>();
    }

    public class FlightStatusSummary
    {
        public string Status { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    public class AirlineFlightSummary
    {
        public string AirlineName { get; set; } = string.Empty;
        public int FlightCount { get; set; }
    }
}
