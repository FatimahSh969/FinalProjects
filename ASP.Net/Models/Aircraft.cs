using System.ComponentModel.DataAnnotations;

namespace SmartAirPort.Models
{
    public class Aircraft
    {

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string RegistrationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Model { get; set; }

        [Required]
        [StringLength(100)]
        public string Manufacturer { get; set; }

        [Range(1,1000)]
        public int Capacity { get; set; }

        [Required]
        public string Status { get; set; }
        public bool IsActive { get; set; }
    }
}
