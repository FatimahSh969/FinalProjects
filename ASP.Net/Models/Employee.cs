using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace SmartAirPort.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your name.")]
        [StringLength(100, ErrorMessage = "This Name is Too Long")]
        public string Name { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter your phone number.")]
        public string Phone { get; set; }
        public string ? Address { get; set; }

        [Required(ErrorMessage = "Please enter your salary.")]
        [Range(3000, 10000, ErrorMessage = "Salary must be between 3000 and 10000.")]
        public double Salary { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        public Department? Department { get; set; }
        
        [ForeignKey("Role")]
        public int? RoleId { get; set; }
        public Role? Role { get; set; }

        public int? IsActiveate { get; set; } = 1;
        public int? DeActiveateby { get; set; }
        public DateTime DeActiveateOn { get; set; }
        public int IsDeleted { get; set; } = 0;
        public int IsDeleteBy { get; set; }
        public DateTime IsDeleteOn { get; set; }
        public int IsDeleteOnBy { get; set; }
        public int IsUpdated { get; set; } = 0;
        public int IsUpdatedBy { get; set; }
        public DateTime IsUpdatedOn { get; set; }

    }
}
