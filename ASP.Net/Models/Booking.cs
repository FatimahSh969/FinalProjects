
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAirPort.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string BookingReference { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string PassengerName { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string PassengerIdNumber { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(20)]
        public string ContactNumber { get; set; } = string.Empty;

        [Required]
        public DateTime BookingDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Confirmed";

        [Required]
        public int FlightId { get; set; }

        [ForeignKey(nameof(FlightId))]
        [ValidateNever]
        public Flight? Flight { get; set; }
    }
}
