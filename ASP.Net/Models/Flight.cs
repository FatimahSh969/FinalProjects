using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SmartAirPort.Models
{
    public class Flight
    {

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string FlightNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string ArrivalAirport { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string DepartureAirport { get; set; } = string.Empty;

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }
        [Required]
        public int AirlineId { get; set; }

        [ValidateNever]
        public Airline Airline { get; set; } = null!;
        public int? AircraftId { get; set; }
        public Aircraft? Aircraft { get; set; }

        public int? GateId { get; set; }
        public Gate? Gate { get; set; }

        public ICollection<Booking> Bookings { get; set; }
         = new List<Booking>();
    }
}
