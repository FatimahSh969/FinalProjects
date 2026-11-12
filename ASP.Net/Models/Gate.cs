using System.ComponentModel.DataAnnotations;

namespace SmartAirPort.Models
{
    public class Gate
    {

        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string GateNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Terminal { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string GateType { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Available";
        public bool IsActive { get; set; } = true;
    }
}
