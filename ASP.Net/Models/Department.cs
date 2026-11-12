using System.ComponentModel.DataAnnotations;

namespace SmartAirPort.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(100)]
        public string Name { get; set; }=string.Empty;

        public ICollection<Employee>? Employees { get; set; } = new List<Employee>();


    }
}
