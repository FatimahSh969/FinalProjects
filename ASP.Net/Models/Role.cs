using System.ComponentModel.DataAnnotations;

namespace SmartAirPort.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        public ICollection<Permission>? Permissions { get; set; }
        public ICollection<User>? Users { get; set; }


    }
}
